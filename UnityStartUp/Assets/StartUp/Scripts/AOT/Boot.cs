using CycloneGames.Service;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

public class Boot : MonoBehaviour
{
    [Inject] private IAddressablesService addressablesService;
    private void Awake()
    {
        
    }

    private void Start()
    {
        EnterGameScene();
    }

    void EnterGameScene()
    {
        DelayEnterGameScene(100).Forget();
    }

    async UniTask DelayEnterGameScene(int milliSecond)
    {
        await UniTask.Delay(milliSecond);
        await addressablesService.LoadSceneAsync("Assets/StartUp/Scenes/Scene_StartUp.unity", AddressablesManager.SceneLoadMode.Additive);
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }
}