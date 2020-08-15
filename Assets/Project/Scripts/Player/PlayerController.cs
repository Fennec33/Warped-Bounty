using UnityEngine;
using UnityEngine.InputSystem;
using WarpedBounty.Player;

namespace WarpedBounty.Player
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerWeapons))]
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerController : MonoBehaviour, InputMaster.IGameplayActions
    {
        private InputMaster _inputMaster;
        private PlayerMovement _playerMovement;
        private PlayerWeapons _playerWeapons;
        private PlayerHealth _playerHealth;

        private void Awake()
        {
            _inputMaster = new InputMaster();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerWeapons = GetComponent<PlayerWeapons>();
            _playerHealth = GetComponent<PlayerHealth>();
            
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

        public void OnShoot(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            _playerWeapons.Shoot();
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            _playerMovement.UpDown(context.ReadValue<float>());
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