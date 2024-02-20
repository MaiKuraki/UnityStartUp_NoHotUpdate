using MessagePipe;
using Zenject;

namespace CycloneGames.UIFramework
{
    public class UIMessage
    {
        public string MessageCode;
        public string[] Params;
    }
    public interface IUIService
    {
        void PublishUIMessage(UIMessage uiMsg);
        void OpenUI(string PageName);
        void CloseUI(string PageName);
    }
    public class UIService : IUIService, IInitializable
    {
        private const string DEBUG_FLAG = "[UIService]";
        
        [Inject] private IPublisher<UIMessage> uiMsgPub;
        [Inject] private DiContainer diContainer;
        
        private UnityEngine.GameObject uiManagerGameObject;
        private UIManager uiManager;
        
        public void Initialize()
        {
            uiManagerGameObject = new UnityEngine.GameObject("UIService");
            uiManager = diContainer.InstantiateComponent<UIManager>(uiManagerGameObject);
            UnityEngine.GameObject.DontDestroyOnLoad(uiManagerGameObject);
        }

        public void PublishUIMessage(UIMessage uiMsg)
        {
            uiMsgPub.Publish(uiMsg);
        }

        public void OpenUI(string PageName)
        {
            if (uiManager == null)
            {
                UnityEngine.Debug.Log($"{DEBUG_FLAG} Invalid UIManager");
            }
            
            uiManager.OpenUI(PageName);
        }

        public void CloseUI(string PageName)
        {
            if (uiManager == null)
            {
                UnityEngine.Debug.Log($"{DEBUG_FLAG} Invalid UIManager");
            }
            
            uiManager.CloseUI(PageName);
        }
    }
}