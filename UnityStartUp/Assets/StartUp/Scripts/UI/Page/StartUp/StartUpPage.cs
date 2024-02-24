using System;
using CycloneGames.UIFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StartUp.UI
{
    public class StartUpPage : UIPage
    {
        [SerializeField] private TMP_Text Text_Version;

        [SerializeField] private Button Btn_NewGame;
        [SerializeField] private Button Btn_ContinuePlay;
        [SerializeField] private Button Btn_Tutorial;
        [SerializeField] private Button Btn_Credit;
        [SerializeField] private Button Btn_Exit;
        public event Action OnClickNewGame;
        public event Action OnClickContinuePlay;
        public event Action OnClickTutorial;
        public event Action OnClickCredit;
        public event Action OnClickExitGame;

        protected override void Awake()
        {
            base.Awake();
            
            Btn_NewGame.onClick.AddListener(() => OnClickNewGame?.Invoke());
            Btn_ContinuePlay.onClick.AddListener(() => OnClickContinuePlay?.Invoke());
            Btn_Tutorial.onClick.AddListener(() => OnClickTutorial?.Invoke());
            Btn_Credit.onClick.AddListener(() => OnClickCredit?.Invoke());
            Btn_Exit.onClick.AddListener(() => OnClickExitGame?.Invoke());
        }
    }
}