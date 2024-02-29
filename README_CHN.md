# Unity初始项目模板(无热更新模块)
<p align="center">
    <br> <a href="README.md">English</a> | 中文
</p>

## 关于
该项目基于 DI/IoC 框架，提供了一套类似于虚幻引擎 GameplayFramework 的类型设计。它包括一个简易的分层 UI 框架以及一个自动化的打包脚本。适用于非热更新的项目，比如 GameJam 的启动项目或其它单机项目。
## 文件目录结构
-   Assets/CycloneGames (可剔除)
    -   该程序集提供了类似于虚幻引擎 GameplayFramework 的框架设计，包含 GameInstance、World、GameMode、PlayerController 和 PlayerState 等类型。对于熟悉虚幻引擎的用户，提供了舒适的过渡。
    -   [README](./UnityStartUp/Assets/CycloneGames/README_CHN.md)
-   Assets/CycloneGames.Service
    -   该程序集提供了资源管理（Addressable）和显示管理（GraphicsSettings）等服务。
    -   [README](./UnityStartUp/Assets/CycloneGames.Service/README_CHN.md)
-   Assets/CycloneGames.UIFramework
    -   该程序集提供了一个简易的分层 UI 框架。
    -   它依赖于 CycloneGames.Service 中的 Addressable 用于加载 UI Prefab，目前无法消除依赖关系。
    -   [README](./UnityStartUp/Assets/CycloneGames.UIFramework/README_CHN.md)
-   Assets/StartUp
    -   该文件夹是 Sample 项目的目录，提供了一个启动场景，开始场景和 Gameplay 场景，用作游戏流程测试。
    -   [README](./UnityStartUp/Assets/StartUp/README.md)