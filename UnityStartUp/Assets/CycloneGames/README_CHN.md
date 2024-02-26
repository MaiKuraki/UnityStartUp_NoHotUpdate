## CycloneGames.GameFramework
### 关于
该框架中的类主要旨在模仿虚幻引擎的命名和某些类似功能。然而，它并没有完全复制虚幻引擎的完整框架，因此其中的内容可能不是完全可靠的。这些类只代表了作者（mai.k@live.com）最初的尝试和个人想法。

### 框架预览
待补充

### 教程
-   
-   Get **GameMode** / **PlayerController**
    -   确保将 GameModeInstaller 预制件附加到场景 SceneContext 的 Prefab Installer 列表中。
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

### 注意事项
-   'Prefabs' 和 'ScriptableObject' 文件夹必须是 Addressable.
-   如果要使用 CameraManager，请确保将 'CinemachineBrain' 组件添加到您的目标摄像机上。