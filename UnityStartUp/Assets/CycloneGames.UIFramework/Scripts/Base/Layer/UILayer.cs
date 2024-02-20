using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CycloneGames.UIFramework
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public class UILayer : MonoBehaviour
    {
        private const string DEBUG_FLAG = "[UILayer]";
        
        [SerializeField] private string layerName;

        private Canvas uiCanvas;
        public Canvas UICanvas => uiCanvas;
        private GraphicRaycaster graphicRaycaster;
        private GraphicRaycaster PageGraphicRaycaster => graphicRaycaster;
        public string LayerName => layerName;

        private List<UIPage> uiPagesList = new List<UIPage>();
        private bool bFinishedLayerInit = false;

        protected void Awake()
        {
            uiCanvas = GetComponent<Canvas>();
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            
            PageGraphicRaycaster.blockingMask = LayerMask.GetMask("UI");

            InitLayer();
        }

        private void InitLayer()
        {
            if (transform.childCount == 0)
            {
                bFinishedLayerInit = true;
                Debug.Log($"{DEBUG_FLAG} Finished init Layer: {LayerName}");
                return;
            }
            
            // Ensure the page's Name matches its associated prefab name, 
            // and that the page's Name is defined within the PageName class.
            uiPagesList = GetComponentsInChildren<UIPage>().ToList();
            foreach (UIPage page in uiPagesList)
            {
                page.SetPageName(page.gameObject.name);
            }
            
            SortPagesByPriority();
            bFinishedLayerInit = true;
            Debug.Log($"{DEBUG_FLAG} Finished init Layer: {LayerName}");
        }

        public void AddPage(UIPage newPage)
        {
            //  NOTE: Ensure the uiPageList is sorted.
            if (!bFinishedLayerInit)
            {
                Debug.LogError($"{DEBUG_FLAG} layer not init, current layer: {LayerName}");
                return;
            }
            
            newPage.gameObject.name = newPage.PageName;
            Transform pageTransform = newPage.transform;
            pageTransform.SetParent(transform, false);
            
            if (uiPagesList.Count == 0)
            {
                uiPagesList.Add(newPage);
                return;
            }
            
            // 逆向遍历列表以找到最后一个Priority等于新页面Priority的索引
            int insertIndex = uiPagesList.Count; // 初始化为列表末尾

            for (int i = uiPagesList.Count - 1; i >= 0; i--) 
            {
                if (uiPagesList[i].Priority > newPage.Priority) 
                {
                    // 找到一个Priority更大的页面，将新页面插入到它前面
                    insertIndex = i;
                } 
                else if (uiPagesList[i].Priority == newPage.Priority) 
                {
                    // 找到一个Priority相同的页面，插入到这个页面之后
                    insertIndex = i + 1;
                    break; // 因为列表是排序的，所以可以中断循环
                }
            }

            // 插入新页面到计算出的插入点
            uiPagesList.Insert(insertIndex, newPage);

            // 只需更新新页面和所有之后页面的层级位置
            for (int i = insertIndex; i < uiPagesList.Count; i++) 
            {
                uiPagesList[i].transform.SetSiblingIndex(i);
            }
        }

        public void RemovePage(string InPageName)
        {
            //  NOTE: Ensure the uiPageList is init.
            if (!bFinishedLayerInit)
            {
                Debug.LogError($"{DEBUG_FLAG} layer not init, current layer: {LayerName}");
                return;
            }
            
            UIPage page = TryGetPageByPageName(InPageName);
            if (!page)
            {
                Debug.LogError($"{DEBUG_FLAG} Remove Page Failure, PageName: {InPageName}");
                return;
            }
            
            uiPagesList.Remove(page);
            page.ClosePage();
        }

        private UIPage TryGetPageByPageName(string InPageName)
        {
            //  Make sure the PageName in uiPageList is Unique
            foreach (UIPage page in uiPagesList)
            {
                if (page.PageName == InPageName)
                {
                    return page;
                }
            }

            return null;
        }
        
        private void SortPagesByPriority()
        {
            if(uiPagesList.Count <= 1) return;
            
            #region Debug origin PageList Info
            // for (int i = 0; i < uiPagesList.Count; i++)
            // {
            //     Debug.Log($"{DEBUG_FLAG} pageName: {uiPagesList[i].PageName}, Priority:{uiPagesList[i].Priority}, idx: {i}");
            // }
            #endregion
            
            // for (int i = 1; i < uiPagesList.Count; i++)
            // {
            //     UIPage current = uiPagesList[i];
            //     int j = i - 1;
            //
            //     // 更改排序条件，确保Priority较大的在后面
            //     while (j >= 0 && uiPagesList[j].Priority > current.Priority)
            //     {
            //         uiPagesList[j + 1] = uiPagesList[j];
            //         j--;
            //     }
            //
            //     uiPagesList[j + 1] = current;
            // }
            
            uiPagesList = uiPagesList.OrderBy(page => page.Priority).ToList();

            #region Debug Sorted PageList Info
            // for (int i = 0; i < uiPagesList.Count; i++)
            // {
            //     Debug.Log($"{DEBUG_FLAG} sorted pageName: {uiPagesList[i].PageName}, Priority:{uiPagesList[i].Priority}, idx: {i}");
            // }
            #endregion

    
            for (int i = 0; i < uiPagesList.Count; i++)
            {
                uiPagesList[i].transform.SetSiblingIndex(i);
            }
        }
    }
}