using CycloneGames.GameFramework;
using CycloneGames.Service;
using CycloneGames.UIFramework;
using Cysharp.Threading.Tasks;
using StartUp.GameSubSystem;
using StartUp.UI;
using Zenject;

public class SceneLogic_Launch : SceneLogic
{
    [Inject] private IAddressablesService addressablesService;
    [Inject] private ISceneManagementService sceneManagementService;
    [Inject] private IUIService uiService;

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
        sceneManagementService.OpenSceneAsync(new SceneLoadParam[] { new SceneLoadParam()
        {
            SceneKey = "Scene_StartUp",
            Priority = 100
        } }, PageName.SimpleLoadingPage, 2000,new []{"Scene_Launch"});
    }
    
    private void OnDestroy()
    {
        uiService.CloseUI(PageName.TitlePage);
    }
}
