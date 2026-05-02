using BerserkPixel.StateMachine;
using Player.Input;
using UnityEngine;
using UnityEngine.Assertions;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [SerializeField] private PlayerInput _playerInput;
        // this is the Transform we want to rotate on the Y axis when changing directions
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Rigidbody _rigidbody;

        // this Vector2 can be used on each State to determine any change
        public Vector2 Movement { get; private set; }

        // since our sprite is facing right, we set it to true
        private bool _isFacingRight = true;
        public bool IsFacingRight { get { return _isFacingRight;}}
        public bool RollPressed;
        private bool _attackPressed;
        public bool AttackPressed {  get { return _attackPressed; }}
        private SpriteRenderer _spriteRenderer;
        private Transform _attackObject;
        
        private void OnEnable()
        {
            _playerInput.MovementEvent += HandleMove;
            _playerInput.RollEvent += HandleRoll; // subsribe
            _playerInput.AttackEvent += HandleAttack;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(_spriteRenderer,"The Player must have a Sprite Renderer");
            _attackObject = transform.GetChild(0);
        }

        private void OnDisable()
        {
            _playerInput.MovementEvent -= HandleMove;
            _playerInput.RollEvent -= HandleRoll; 
            _playerInput.AttackEvent -= HandleAttack;
        }

        private void HandleRoll(bool isPressed)
        {
            RollPressed = isPressed;
        }
        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
            CheckFlipSprite(movement);
        }
        private void HandleAttack(bool isPressed)
        {
            _attackPressed = isPressed;
        }

        private void CheckFlipSprite(Vector3 velocity)
        {
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
            
            _isFacingRight = !_isFacingRight;
            _attackObject.Rotate(_spriteTransform.rotation.x, 180f, _spriteTransform.rotation.z);
            _spriteRenderer.flipX = _isFacingRight;
        }

        // just  a simple implementation of movement by setting the velocity of the Rigidbody
        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}