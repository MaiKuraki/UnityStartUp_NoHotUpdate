# CAUTION
## This repository is deprecated, please check out new repo at [[New Repo]](https://github.com/MaiKuraki/UnityStarter.git)

# Unity StartUp Project Template(No HotUpdate Module)
<p align="center">
    <br> English | <a href="README_CHN.md">中文</a>
</p>

## Overview
This project is built upon a DI/IoC framework, offering a type design reminiscent of Unreal Engine's GameplayFramework. It includes a straightforward hierarchical UI framework and an automated packaging script. Tailored for projects not requiring hot updates, such as startup projects for Game Jams or other standalone endeavors.
## File Directory Structure
-   Assets/CycloneGames (Optional Exclusion)
    -   This assembly provides a framework design akin to Unreal Engine's GameplayFramework, featuring types such as GameInstance, World, GameMode, PlayerController, and PlayerState. It offers a comfortable transition for users familiar with Unreal Engine.
    -   [README](./UnityStartUp/Assets/CycloneGames/README.md)
-   Assets/CycloneGames.Service
    -   This assembly delivers services such as resource management (Addressable) and display management (GraphicsSettings).
    -   [README](./UnityStartUp/Assets/CycloneGames.Service/README.md)
-   Assets/CycloneGames.UIFramework
    -   This assembly offers a simple hierarchical UI framework.
    -   It depends on Addressable from CycloneGames.Service for loading UI Prefabs, and currently, the dependency cannot be eliminated.
    -   [README](./UnityStartUp/Assets/CycloneGames.UIFramework/README.md)
-   Assets/StartUp
    -   This folder serves as the directory for the Sample project, offering a startup scene, initial scenes, and gameplay scenes for game flow testing.
    -   [README](./UnityStartUp/Assets/StartUp/README.md)
