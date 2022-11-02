using System;
using System.Collections.Generic;
using Assets.Code.Map;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Code.StaticClass
{
    public class ContractController : MonoBehaviour
    {
        [SerializeField] private Button _contract1;
        [SerializeField] private Button _contract2;
        [SerializeField] private Button _contract3;
        [SerializeField] private Button _contract4;
        [SerializeField] private Button _contract5;

        private ControllerMap _map;
        private Dictionary<Button, Contract> _contracts;

        public void Construct(ControllerMap map)
        {
            _map = map;
            _contracts = new Dictionary<Button, Contract>()
            {
                { _contract1, Contract.ContratsOneHour },
                { _contract2, Contract.ContratsThreeHour },
                { _contract3, Contract.ContratsSixHour },
                { _contract4, Contract.ContratsNineHour  },
                { _contract5, Contract.ContratsTwelveHour  }
            };
            Subscribe();
        }

        private void Subscribe()
        {
            _contract1.onClick.AddListener(() => OnClick(_contract1));
            _contract2.onClick.AddListener(() => OnClick(_contract2));
            _contract3.onClick.AddListener(() => OnClick(_contract3));
            _contract4.onClick.AddListener(() => OnClick(_contract4));
            _contract5.onClick.AddListener(() => OnClick(_contract5));
        }

        private void OnClick(Button button)
        {
            var contract = _contracts[button];
            _map.SelectContract(contract);
        }
    }
}