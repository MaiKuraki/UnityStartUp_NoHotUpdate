using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace CycloneGames.Service
{
    public class AddressablesManager : MonoBehaviour
    {
        public enum SceneLoadMode
        {
            Single,
            Additive
        }

        public enum AssetHandleReleasePolicy
        {
            Keep,
            ReleaseOnComplete
        }

        private const string DEBUG_FLAG = "[AddressablesManager]";

        // ConcurrentDictionary to ensure thread safety when accessing activeHandles.
        private ConcurrentDictionary<string, AsyncOperationHandle> activeHandles =
            new ConcurrentDictionary<string, AsyncOperationHandle>();

        // Loads an asset asynchronously and returns a UniTask.
        public UniTask<TResultObject> LoadAssetAsync<TResultObject>(string key, AssetHandleReleasePolicy releasePolicy,
            CancellationToken cancellationToken = default) where TResultObject : UnityEngine.Object
        {
            var completionSource = new UniTaskCompletionSource<TResultObject>();
            var operationHandle = Addressables.LoadAssetAsync<TResultObject>(key);

            operationHandle.Completed += operation =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    ReleaseAssetHandleIfNeeded(key, operationHandle, releasePolicy);
                    return;
                }

                try
                {
                    if (operation.Status == AsyncOperationStatus.Succeeded)
                    {
                        // Store the handle in the dictionary if needed
                        if (releasePolicy == AssetHandleReleasePolicy.Keep)
                        {
                            activeHandles[key] = operationHandle;
                        }

                        completionSource.TrySetResult(operation.Result);
                    }
                    else
                    {
                        var errorMessage = $"Failed to load the asset with key {key}. Status: {operation.Status}";
                        if (operation.OperationException != null)
                        {
                            errorMessage += $", Exception: {operation.OperationException.Message}";
                        }

                        completionSource.TrySetException(new Exception(errorMessage));
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{DEBUG_FLAG} Exception occurred: {ex.Message}");
                    completionSource.TrySetException(ex);
                }
                finally
                {
                    if (releasePolicy != AssetHandleReleasePolicy.Keep)
                    {
                        ReleaseAssetHandleIfNeeded(key, operationHandle, releasePolicy);
                    }
                }
            };

            RegisterForCancellation(key, operationHandle, cancellationToken);
            return completionSource.Task;
        }

        // Method to release a handle by key
        public void ReleaseAssetHandle(string key)
        {
            if (activeHandles.TryRemove(key, out AsyncOperationHandle handle))
            {
                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
            else
            {
                Debug.LogWarning($"{DEBUG_FLAG} No handle found for key: {key}");
            }
        }

        // Releases an asset using its handle if the policy dictates.
        private void ReleaseAssetHandleIfNeeded(string key, AsyncOperationHandle handle,
            AssetHandleReleasePolicy releasePolicy)
        {
            if (releasePolicy == AssetHandleReleasePolicy.ReleaseOnComplete && handle.IsValid())
            {
                Addressables.Release(handle);
                activeHandles.TryRemove(key, out _);
            }
        }

        // Cancels the asset load operation if the CancellationToken is invoked.
        private void RegisterForCancellation(string key, AsyncOperationHandle handle,
            CancellationToken cancellationToken)
        {
            // Register a callback with the cancellation token that will release the handle if the token is cancelled.
            var registration = cancellationToken.Register(() =>
            {
                if (handle.IsValid() && !handle.IsDone)
                {
                    Addressables.Release(handle);
                    activeHandles.TryRemove(key, out _);
                }
            });

            // To prevent the callback from remaining registered after the task is complete, we unregister it upon completion.
            handle.Completed += _ => registration.Dispose();
        }
        
        public UniTask<SceneInstance> LoadSceneAsync(string key, SceneLoadMode sceneLoadMode = SceneLoadMode.Single,
            bool activateOnLoad = true, int priority = 100,
            AssetHandleReleasePolicy releasePolicy = AssetHandleReleasePolicy.Keep,
            CancellationToken cancellationToken = default)
        {
            var completionSource = new UniTaskCompletionSource<SceneInstance>();
            LoadSceneMode loadSceneMode =
                sceneLoadMode == SceneLoadMode.Single ? LoadSceneMode.Single : LoadSceneMode.Additive;
            var operationHandle = Addressables.LoadSceneAsync(key, loadSceneMode, activateOnLoad, priority);

            operationHandle.Completed += operation =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    completionSource.TrySetCanceled();
                    ReleaseAssetHandleIfNeeded(key, operationHandle, releasePolicy);
                    return;
                }

                try
                {
                    if (operation.Status == AsyncOperationStatus.Succeeded)
                    {
                        if (releasePolicy == AssetHandleReleasePolicy.Keep)
                        {
                            activeHandles[key] = operationHandle;
                        }

                        completionSource.TrySetResult(operation.Result);
                    }
                    else
                    {
                        var errorMessage = $"Failed to load the scene with key {key}. Status: {operation.Status}";
                        if (operation.OperationException != null)
                        {
                            errorMessage += $", Exception: {operation.OperationException.Message}";
                        }

                        completionSource.TrySetException(new Exception(errorMessage));
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{DEBUG_FLAG} Exception occurred: {ex.Message}");
                    completionSource.TrySetException(ex);
                }
                finally
                {
                    if (releasePolicy != AssetHandleReleasePolicy.Keep)
                    {
                        ReleaseAssetHandleIfNeeded(key, operationHandle, releasePolicy);
                    }
                }
            };

            RegisterForCancellation(key, operationHandle, cancellationToken);
            return completionSource.Task;
        }
    }
}