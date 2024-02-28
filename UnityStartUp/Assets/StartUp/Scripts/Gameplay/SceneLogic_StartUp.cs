using CycloneGames.GameFramework;
using CycloneGames.UIFramework;
using Cysharp.Threading.Tasks;
using StartUp.GameSubSystem;
using StartUp.UI;
using Zenject;

namespace StartUp.Gameplay
{
    public class SceneLogic_StartUp : SceneLogic
    {
        [Inject] private DiContainer diContainer;
        [Inject] private IUIService uiService;
        [Inject] private ISceneManagementService sceneManagementService;
        
        StartUpPage startUpPage;

        protected override void Start()
        {
            base.Start();
            
            StartDemo();
        }

        void StartDemo()
        {
            uiService.OpenUI(UI.PageName.StartUpPage, OnPageCreated: BindStartUpPageEvents);
        }
        
        void BindStartUpPageEvents(UIPage uiPage)
        {
            StartUpPage startUpPage = uiPage as StartUpPage;
            if (startUpPage)
            {
                startUpPage.OnClickNewGame -= EnterGameplay;
                startUpPage.OnClickNewGame += EnterGameplay;
            }
        }
        
        void EnterGameplay()
        {
            uiService.CloseUI(UI.PageName.StartUpPage);
            
            sceneManagementService.OpenSceneAsync(new SceneLoadParam[] { new SceneLoadParam()
            {
                SceneKey = "Scene_Gameplay",
                Priority = 100
            } }, PageName.SimpleLoadingPage, 2000,new []{"Scene_StartUp"}, null, () =>
            {
                
            });
        }
    }
}

