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
        //[SerializeField] private float startChargeingShot;
        [SerializeField] private float finishChargingShot = 1.5f;
        
        
        private InputMaster _inputMaster;
        private PlayerMovement _playerMovement;
        private PlayerWeapons _playerWeapons;
        private PlayerHealth _playerHealth;
        private bool _chargingShot = false;
        private float _chargeTime = 0f;

        private void Awake()
        {
            _inputMaster = new InputMaster();
            _playerMovement = GetComponent<PlayerMovement>();
            _playerWeapons = GetComponent<PlayerWeapons>();
            _playerHealth = GetComponent<PlayerHealth>();
            
            _inputMaster.Gameplay.SetCallbacks(this);
        }

        private void Update()
        {
            if (_chargingShot)
            {
                _chargeTime += Time.deltaTime;
            }
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
            if (context.canceled)
                _playerMovement.StopJump();
            
        }

        public void OnShoot(InputAction.CallbackContext context)
        {

            if (context.performed)
            {
                _chargeTime = 0f;
                _chargingShot = true;
            }

            if (context.canceled)
            {
                if (_chargeTime < finishChargingShot)
                    _playerWeapons.Shoot();
                else
                    _playerWeapons.ChargeShoot();
                _chargingShot = false;
            }
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