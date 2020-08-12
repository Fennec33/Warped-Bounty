using UnityEngine;
using UnityEngine.InputSystem;

namespace WarpedBounty.Player
{
    public class PlayerController : MonoBehaviour, InputMaster.IGameplayActions
    {
        private InputMaster _inputMaster;
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _inputMaster = new InputMaster();
            _playerMovement = GetComponent<PlayerMovement>();
            
            _inputMaster.Gameplay.SetCallbacks(this);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector3 direction = new Vector3(context.ReadValue<float>(),0f,0f);
            _playerMovement.Move(direction);
        }

        private void OnEnable()
        {
            _inputMaster.Enable();
        }

        private void OnDisable()
        {
            _inputMaster.Disable();
        }
    }
}