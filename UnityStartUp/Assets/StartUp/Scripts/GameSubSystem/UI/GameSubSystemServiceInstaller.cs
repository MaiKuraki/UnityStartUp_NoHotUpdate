using MessagePipe;
using Zenject;
using CycloneGames.UIFramework;

namespace StartUp.GameSubSystem
{
    public class GameSubSystemServiceInstaller : MonoInstaller
    {
        [Inject] private MessagePipeOptions msgOpt;
        
        public override void InstallBindings()
        {
            UIFramework uiFramework = UnityEngine.GameObject.FindObjectOfType<UIFramework>();
            UnityEngine.GameObject.DontDestroyOnLoad(uiFramework);
            Container.BindInstance(uiFramework).AsSingle();
            Container.QueueForInject(uiFramework);
            
            UIRoot uiRoot = UnityEngine.GameObject.FindObjectOfType<UIRoot>();
            Container.BindInstance(uiRoot).AsSingle();
            Container.QueueForInject(uiRoot);
            
            Container.BindInterfacesTo<UIService>().AsSingle().NonLazy();
            Container.BindMessageBroker<UIMessage>(options: msgOpt);
        }
    }
}

