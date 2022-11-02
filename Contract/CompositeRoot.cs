using Assets.Code.Map;
using UnityEngine;

namespace Assets.Code.StaticClass
{
    public class CompositeRoot : MonoBehaviour
    {
        [SerializeField] private ControllerMap _map;
        [SerializeField] private ContractController _controller;

        private void Start()
        {
            _controller.Construct(_map);
        }
    }
}