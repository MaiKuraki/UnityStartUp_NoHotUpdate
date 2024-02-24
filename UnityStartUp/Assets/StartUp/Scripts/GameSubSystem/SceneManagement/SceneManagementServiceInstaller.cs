using Zenject;

namespace StartUp.GameSubSystem
{
    public class SceneManagementServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SceneManagementService>().AsSingle().NonLazy();
        }
    }
}