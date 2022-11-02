using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.MainMenu
{
    public class MainHeadlerSetapp : MonoBehaviour
    {
        [field: SerializeField] private DataMainMenuHeader _dataMainMenuHeader = new DataMainMenuHeader();
        public static Action EventUpdateHeaderMainMenu { get; set; }
        public static Action EventUpdateImageSetApp { get; set; }

        [Field: SerializeField] GameObject Panel { get; set; }
        private void Awake()
        {
            EventUpdateHeaderMainMenu += OnEnable;
            EventUpdateImageSetApp += UpdateImageSetAppPlayer;
        }
        private void Start()
        {
            UpdateImageSetAppPlayer();
        }
        public void AddSetup() //TODO  3: Добавить покупку доп слотов или т.п
        {
            if (PlayerData.instanse.instanseSaveMoneyPlayer.Drive > 100)
            {
                PlayerData.instanse.instanseSaveCard.AllSetup++;
                PlayerData.instanse.instanseSaveMoneyPlayer.Drive -= 100;
                StartCoroutine(StartPanel());
            }
        }
        
        private void OnDestroy()
        {
            EventUpdateHeaderMainMenu -= OnEnable;
            EventUpdateImageSetApp -= UpdateImageSetAppPlayer;
        }
        private void OnEnable() //MainLogic
        {
            StartCoroutine(StartPanel());
        }
        IEnumerator StartPanel()
        {
            yield return new WaitForSeconds(0.1f);
            var a = PlayerData.instanse.instanseSaveCard.AllSetup;
            for (int i = 0; i < a; i++)
            {
                _dataMainMenuHeader.SetTextAppAdd[i].enabled = false;
            }

            UpdateImageSetAppPlayer();

            for (int i = 0; i < a; i++)
            {
                _dataMainMenuHeader.SetupButtonAdd[i].enabled = false;
            }
            _dataMainMenuHeader.SetImageCurrentSetup[PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer - 1].enabled = true;

        }
        private void UpdateImageSetAppPlayer()
        {
            for (int i = 0; i < _dataMainMenuHeader.SetImageCurrentSetup.Length; i++)
            {
                _dataMainMenuHeader.SetImageCurrentSetup[i]?.gameObject.SetActive(false);
            }
            var a = PlayerData.instanse.instanseSaveCard.CurrentSetupPlayer;
            _dataMainMenuHeader.SetImageCurrentSetup[a-1].gameObject.SetActive(true);
        }
    }
}
[Serializable]
public class DataMainMenuHeader 
{
    [field: SerializeField] public Text[] SetTextAppAdd { get; set; } = new Text[5];
    [field: SerializeField] public Image[] SetImageCurrentSetup { get; set; } = new Image[5];
    [field: SerializeField] public Button[] SetupButtonAdd { get; set; } = new Button[5];
    
}
