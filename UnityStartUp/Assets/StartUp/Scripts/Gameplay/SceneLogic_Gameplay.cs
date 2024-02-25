using CycloneGames.GameFramework;
using CycloneGames.UIFramework;
using Zenject;

namespace StartUp.Gameplay
{
    public class SceneLogic_Gameplay : SceneLogic
    {
        [Inject] private IUIService uiService;

        protected override void Start()
        {
            base.Start();
            
            uiService.OpenUI(UI.PageName.GameplayMenuPage);
        }
    }
}
