using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WarpedBounty.Player
{
    #region Component Requirements
    [RequireComponent(typeof(PlayerInfo))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerWeapons))]
    [RequireComponent(typeof(PlayerHealth))]
    #endregion
    public class PlayerController : MonoBehaviour, InputMaster.IGameplayActions
    {
        [SerializeField] private float finishChargingShot = 1.5f;
        
        private InputMaster _inputMaster;
        private PlayerMovement _playerMovement;
        private PlayerWeapons _playerWeapons;
        private PlayerHealth _playerHealth;
        private Timer _chargeTimer;

        private void Awake()
        {
            _inputMaster = new InputMaster();
            _inputMaster.Gameplay.SetCallbacks(this);
            
            _playerMovement = GetComponent<PlayerMovement>();
            _playerWeapons = GetComponent<PlayerWeapons>();
            _playerHealth = GetComponent<PlayerHealth>();

            _chargeTimer = gameObject.AddComponent<Timer>();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.started) return;
            var direction = new Vector2(context.ReadValue<float>(),0f);
            _playerMovement.Move(direction);
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.performed)
                _playerMovement.Jump();
            if (context.canceled)
                _playerMovement.StopJump();
            
        }

        public void OnShoot(InputAction.CallbackContext context)
        {

            if (context.performed)
            {
                _chargeTimer.ResetTime();
            }

            if (context.canceled)
            {
                if (_chargeTimer.GetTime() < finishChargingShot)
                    _playerWeapons.Shoot();
                else
                    _playerWeapons.ChargeShoot();
            }
        }

        public void OnUpDown(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            
            if (value > 0.2f) _playerMovement.LookUp();
            else if (value < -0.2f) _playerMovement.Duck();
            else _playerMovement.Stand();
        }

        private void OnEnable() => _inputMaster.Enable();
        private void OnDisable() => _inputMaster.Disable();
    }
}