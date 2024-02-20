## CycloneGames.GameFramework
### About
The classes in this framework are primarily designed to mimic the naming and some similar functionalities of the Unreal Engine. However, it does not fully replicate the complete framework of the Unreal Engine, so the contents may not be entirely reliable. These classes only represent the initial attempts and personal ideas of the author (mai.k@live.com).

这个框架中的类主要是为了模仿虚幻引擎的命名和一些相似的工作方式，但并未完全模仿虚幻引擎的完整框架，因此其中的内容可能不完全可靠。这些类仅代表作者（旋风冲锋 mai.k@live.com）个人思路的初步尝试。

### Note
-   The 'Prefabs' and 'ScriptableObject' folders must be Addressable.
-   If you want to use the CameraManager, make sure to add a 'CinemachineBrain' component to your working camera.

### Tutorial
-   Get **GameMode** / **PlayerController**
    -   Ensure the GameModeInstaller prefab is attached to the SceneContext Prefab Installer list in your scene.
    -   ```csharp
        class YourClassInDIFramework
        {
            [Inject] IWorld World;
        
            void Foo()
            {
                GameMode GM = World.GetGameMode();
                PlayerController PC = GM.GetPlayerController();
            }
        }
        ```
