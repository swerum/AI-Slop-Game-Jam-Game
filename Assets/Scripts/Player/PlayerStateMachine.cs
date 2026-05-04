using BerserkPixel.StateMachine;
using Player.Input;
using Player.States;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

namespace Player
{
    
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [Header("Player Attributes")]
        [SerializeField] private InputManager _playerInput;
        [SerializeField] private GameManager _gameManager;
        // this is the Transform we want to rotate on the Y axis when changing directions

        // this Vector2 can be used on each State to determine any change
        public Vector2 Movement { get; private set; }
        private bool _rollPressed;
        public bool RollPressed { get {return _rollPressed; } set { _rollPressed = value; } }
        private bool _attackPressed;
        public bool AttackPressed {  get { return _attackPressed; }}
        public InputManager PlayerInput { get {return _playerInput; }}
        public GameManager GameManager { get {return _gameManager; }}
        private static PlayerStateMachine _instance;
        public static PlayerStateMachine Instance {get {return _instance; }}
        protected override void Awake()
        {
            base.Awake();
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
            }
        }
        
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
        public override void DamageResponse(int totalHealth)
        {
            if (totalHealth <= 0)
            {
                SetState(typeof(PlayerDeathState));
                // _gameManager.OpenMenuFromGameplay(GameState.GameOver);
            } else
            {
                SetState(typeof(PlayerHurtState));
            }
        }
    }
}