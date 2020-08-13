using UnityEngine;
using UnityEngine.InputSystem;
using WarpedBounty.Player;

namespace WarpedBounty.Player
{
    [RequireComponent(typeof(PlayerMovement))]
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

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                _playerMovement.Jump();
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