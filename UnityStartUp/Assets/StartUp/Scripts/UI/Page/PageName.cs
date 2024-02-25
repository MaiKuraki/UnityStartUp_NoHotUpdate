namespace StartUp.UI
{
    /// <summary>
    /// Note: The name is derived from the YooAsset AssetBundle Collector Address. 
    /// For example, in the prefab group, there is an Asset address "Prefab_TitlePage". The rule in YooAsset is "AddressByGroupAndFileName",
    /// while in this class, we only use the FileName, resulting in "TitlePage" without the "Prefab_" prefix.
    /// Make sure the PageName is Unique.
    /// </summary>
    public static class PageName
    {
        //  Launch
        public static readonly string TitlePage = "TitlePage";
        
        //  Loading
        public static readonly string SimpleLoadingPage = "SimpleLoadingPage";
        
        //  Login
        public static readonly string StartUpPage = "StartUpPage";
        
        //  Gameplay
        public static readonly string GameplayMenuPage = "GameplayMenuPage";
    }
}