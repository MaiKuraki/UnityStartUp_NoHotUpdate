using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;
using Zenject;

namespace CycloneGames.Service
{
    public interface IAddressablesService
    {
        /// <summary> 
        /// Loads an asset asynchronously and releases the asynchronous handle immediately after loading completes.
        /// </summary>
        /// <param name="key">The key of the asset to load.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResultObject">The type of the asset to load.</typeparam>
        /// <returns>A UniTask that completes with the loaded asset.</returns>
        UniTask<TResultObject> LoadAssetWithAutoReleaseAsync<TResultObject>(string key,
            CancellationToken cancellationToken = default) where TResultObject : UnityEngine.Object;

        /// <summary>
        /// Loads an asset asynchronously and retains the handle in memory after loading completes.
        /// To prevent memory leaks, ReleaseAssetHandle(key) must be called when the asset is no longer needed.
        /// </summary>
        /// <param name="key">The key of the asset to be loaded.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <typeparam name="TResultObject">The type of the asset to be loaded.</typeparam>
        /// <returns>A UniTask that completes with the loaded asset.</returns>
        UniTask<TResultObject> LoadAssetWithRetentionAsync<TResultObject>(string key,
            CancellationToken cancellationToken = default) where TResultObject : UnityEngine.Object;

        /// <summary>
        /// Loads a scene asynchronously and releases the asynchronous handle immediately after loading completes.
        /// </summary>
        /// <param name="key">The key of the scene to load.</param>
        /// <param name="sceneLoadMode">The mode in which the scene should be loaded, Single or Additive.</param>
        /// <param name="activateOnLoad">Should the scene be activated upon load.</param>
        /// <param name="priority">The loading priority. Higher numbers are higher priority.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A UniTask that completes with the loaded scene instance.</returns>
        UniTask<SceneInstance> LoadSceneWithAutoReleaseAsync(string key,
            AddressablesManager.SceneLoadMode sceneLoadMode = AddressablesManager.SceneLoadMode.Single,
            bool activateOnLoad = true, int priority = 100, CancellationToken cancellationToken = default);

        /// <summary>
        /// Loads a scene asynchronously and retains the handle in memory after loading completes.
        /// To prevent memory leaks, ReleaseAssetHandle(key) must be called when the scene is no longer needed.
        /// </summary>
        /// <param name="key">The key of the scene to be loaded.</param>
        /// <param name="sceneLoadMode">The mode in which the scene should be loaded, Single or Additive.</param>
        /// <param name="activateOnLoad">Should the scene be activated upon load.</param>
        /// <param name="priority">The loading priority. Higher numbers are higher priority.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A UniTask that completes with the loaded scene instance.</returns>
        UniTask<SceneInstance> LoadSceneWithRetentionAsync(string key,
            AddressablesManager.SceneLoadMode sceneLoadMode = AddressablesManager.SceneLoadMode.Single,
            bool activateOnLoad = true, int priority = 100, CancellationToken cancellationToken = default);

        /// <summary>
        /// Releases the handle associated with a previously loaded asset.
        /// </summary>
        /// <param name="key">The key of the asset whose handle is to be released.</param>
        void ReleaseAssetHandle(string key);

        /// <summary>
        /// Checks if the service is ready and the AddressablesManager has been initialized.
        /// </summary>
        /// <returns>True if the service is ready, false otherwise.</returns>
        bool IsServiceReady();
    }

    public class AddressablesService : IInitializable, IAddressablesService
    {
        private const string DEBUG_FLAG = "[AddressablesService]";
        [Inject] private IServiceDisplay serviceDisplay;
        private AddressablesManager addressablesManager;

        public void Initialize()
        {
            // Check if an instance of AddressablesManager already exists to prevent multiple instances.
            if (addressablesManager == null)
            {
                GameObject addressablesManagerGO = new GameObject("AddressablesManager");
                addressablesManagerGO.transform.SetParent(serviceDisplay.ServiceDisplayTransform);
                addressablesManager = addressablesManagerGO.AddComponent<AddressablesManager>();
            }
            else
            {
                Debug.LogWarning($"{DEBUG_FLAG} AddressablesManager is already initialized.");
            }
        }

        public UniTask<TResultObject> LoadAssetWithAutoReleaseAsync<TResultObject>(string key,
            CancellationToken cancellationToken = default) where TResultObject : Object
        {
            if (addressablesManager == null)
            {
                throw new System.InvalidOperationException($"{DEBUG_FLAG} AddressablesManager is not initialized.");
            }

            return addressablesManager.LoadAssetAsync<TResultObject>(key,
                AddressablesManager.AssetHandleReleasePolicy.ReleaseOnComplete, cancellationToken);
        }

        public UniTask<TResultObject> LoadAssetWithRetentionAsync<TResultObject>(string key,
            CancellationToken cancellationToken = default) where TResultObject : Object
        {
            if (addressablesManager == null)
            {
                throw new System.InvalidOperationException($"{DEBUG_FLAG} AddressablesManager is not initialized.");
            }

            return addressablesManager.LoadAssetAsync<TResultObject>(key,
                AddressablesManager.AssetHandleReleasePolicy.Keep, cancellationToken);
        }

        public UniTask<SceneInstance> LoadSceneWithAutoReleaseAsync(string key,
            AddressablesManager.SceneLoadMode sceneLoadMode = AddressablesManager.SceneLoadMode.Single,
            bool activateOnLoad = true, int priority = 100, CancellationToken cancellationToken = default)
        {
            if (addressablesManager == null)
            {
                throw new System.InvalidOperationException($"{DEBUG_FLAG} AddressablesManager is not initialized.");
            }

            return addressablesManager.LoadSceneAsync(key, sceneLoadMode, activateOnLoad, priority,
                AddressablesManager.AssetHandleReleasePolicy.ReleaseOnComplete, cancellationToken);
        }

        public UniTask<SceneInstance> LoadSceneWithRetentionAsync(string key,
            AddressablesManager.SceneLoadMode sceneLoadMode = AddressablesManager.SceneLoadMode.Single,
            bool activateOnLoad = true, int priority = 100, CancellationToken cancellationToken = default)
        {
            if (addressablesManager == null)
            {
                throw new System.InvalidOperationException($"{DEBUG_FLAG} AddressablesManager is not initialized.");
            }

            return addressablesManager.LoadSceneAsync(key, sceneLoadMode, activateOnLoad, priority,
                AddressablesManager.AssetHandleReleasePolicy.Keep, cancellationToken);
        }


        public void ReleaseAssetHandle(string key)
        {
            if (addressablesManager == null)
            {
                throw new System.InvalidOperationException($"{DEBUG_FLAG} AddressablesManager is not initialized.");
            }

            addressablesManager.ReleaseAssetHandle(key);
        }

        public bool IsServiceReady()
        {
            return addressablesManager != null;
        }
    }
}