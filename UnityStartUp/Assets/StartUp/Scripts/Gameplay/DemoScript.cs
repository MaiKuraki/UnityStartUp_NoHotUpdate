using CycloneGames.UIFramework;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace StartUp.Gameplay
{
    public class DemoScript : MonoBehaviour
    {
        [Inject] private IUIService uiService;
        private void Start()
        {
            StartDemo();
        }

        void StartDemo()
        {
            RefreshUI();
        }

        async void RefreshUI()
        {
            uiService.OpenUI(UI.PageName.StartUpPage);
            uiService.CloseUI(UI.PageName.TitlePage);
            await UniTask.Delay(1000);
        }
    }
}