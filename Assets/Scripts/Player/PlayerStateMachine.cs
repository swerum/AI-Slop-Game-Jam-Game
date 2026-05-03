using BerserkPixel.StateMachine;
using Player.Input;
using Player.States;
using UnityEngine;
using UnityEngine.Assertions;

namespace Player
{
    
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [Header("Player Attributes")]
        [SerializeField] private PlayerInput _playerInput;
        // this is the Transform we want to rotate on the Y axis when changing directions

        // this Vector2 can be used on each State to determine any change
        public Vector2 Movement { get; private set; }
        private bool _rollPressed;
        public bool RollPressed { get {return _rollPressed; } set { _rollPressed = value; } }
        private bool _attackPressed;
        public bool AttackPressed {  get { return _attackPressed; }}
        
        
        private void OnEnable()
        {
            _playerInput.MovementEvent += HandleMove;
            _playerInput.RollEvent += HandleRoll; // subsribe
            _playerInput.AttackEvent += HandleAttack;
            
        }

        private void OnDisable()
        {
            _playerInput.MovementEvent -= HandleMove;
            _playerInput.RollEvent -= HandleRoll; 
            _playerInput.AttackEvent -= HandleAttack;
        }

        private void HandleRoll(bool isPressed)
        {
            _rollPressed = isPressed;
        }
        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
        }
        private void HandleAttack(bool isPressed)
        {
            _attackPressed = isPressed;
        }
        public override void SetHurtState()
        {
            SetState(typeof(PlayerHurtState));

        }
    }
}