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
            Container.BindInstance(uiFramework).AsSingle().NonLazy();
            UIRoot uiRoot = UnityEngine.GameObject.FindObjectOfType<UIRoot>();
            Container.BindInstance(uiRoot).AsSingle().NonLazy();
            Container.BindInterfacesTo<UIService>().AsSingle().NonLazy();
            Container.BindMessageBroker<UIMessage>(options: msgOpt);
        }
    }
}

