## CycloneGames.GameFramework
### About
The classes in this framework are primarily designed to mimic the naming and some similar functionalities of the Unreal Engine. However, it does not fully replicate the complete framework of the Unreal Engine, so the contents may not be entirely reliable. These classes only represent the initial attempts and personal ideas of the author (mai.k@live.com).

### Framework Design
to be added

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

### Note
-   The 'Prefabs' and 'ScriptableObject' folders must be Addressable.
-   If you want to use the CameraManager, make sure to add a 'CinemachineBrain' component to your working camera.