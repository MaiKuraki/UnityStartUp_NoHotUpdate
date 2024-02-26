## Note
This assembly is tightly coupled with the CycloneGames.Service assembly and relies on its ResourceManagement (Addressable) and Camera Service. Currently, it cannot be separated from CycloneGames.Service.

## Framework Design
This assembly is designed with a hierarchical structure of UIRoot - Layer - Page, where each Layer manages the automatic sorting of Pages.
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
## Tutorial
### Creating PagePrefab
To be added.

### Creating PageConfig
To be added.