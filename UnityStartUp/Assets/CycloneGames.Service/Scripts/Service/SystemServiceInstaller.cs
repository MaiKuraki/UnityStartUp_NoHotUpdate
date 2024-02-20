#define ENABLE_CHEAT

using Zenject;
using MessagePipe;

namespace CycloneGames.Service
{
    public class SystemServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var opt = Container.BindMessagePipe();

            Container.BindInterfacesTo<ServiceDisplay>().AsSingle().NonLazy();
            Container.BindInterfacesTo<GraphicsSettingService>().AsSingle().NonLazy();

            MainCamera mainCamera = UnityEngine.GameObject.FindObjectOfType<MainCamera>();
            Container.BindInstance(mainCamera).AsSingle().NonLazy();
            UnityEngine.GameObject.DontDestroyOnLoad(mainCamera);
            
#if ENABLE_CHEAT
            Container.Bind<ICheatService>().To<CheatService>()
                .FromNewComponentOnNewGameObject()
                .WithGameObjectName("CheatService")
                .AsSingle().NonLazy();
            Container.BindMessageBroker<CheatMessage>(options: opt);
#endif
        }
    }
}