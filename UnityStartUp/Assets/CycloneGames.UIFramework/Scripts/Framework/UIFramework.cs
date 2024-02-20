using CycloneGames.Service;
using UnityEngine;
using Zenject;

namespace CycloneGames.UIFramework
{
    public class UIFramework : MonoBehaviour
    {
        [Inject] private MainCamera gameMainCamera;
        [SerializeField] private UIRoot uiRoot;
        [SerializeField] private Camera uiCamera;

        private Transform uiRootTF;
        private Transform uiCameraTF;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            uiRootTF = uiRoot.transform;
            uiCameraTF = uiCamera.transform;
        }

        private void Start()
        {
            gameMainCamera.AddNewCameraToStack(uiCamera);
        }

        private void OnDestroy()
        {
            gameMainCamera.RemoveCameraFromStack(uiCamera);
        }
    }
}