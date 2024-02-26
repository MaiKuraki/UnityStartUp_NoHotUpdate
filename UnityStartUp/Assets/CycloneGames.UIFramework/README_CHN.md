## 注意
这个程序集与 CycloneGames.Service 程序集耦合，使用其中的 ResourceManagement（Addressable）和 Camera Service，目前无法将 CycloneGames.Service 拆分。

## 框架设计
这个程序集设计了一套以 UIRoot - Layer - Page 的结构框架，在每个 Layer 中实现了 Page 的自动排序管理。
```
UIRoot
  ├─ Layer 1 (eg. General) 
  │    ├─ Page 1 
  │    └─ Page 2
  ├─ Layer 2 (eg. Notification)
  │    └─ Page 3
  └─ Layer 3 (eg. FullScreenOverlay)
       └─ Page 4
```
## 教程
### 创建 PagePrefab
待补充

### 创建 PageConfig
待补充