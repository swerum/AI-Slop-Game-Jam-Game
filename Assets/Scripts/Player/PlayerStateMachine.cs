using BerserkPixel.StateMachine;
using Player.Input;
using UnityEngine;
using UnityEngine.Assertions;

namespace Player
{
    public class PlayerStateMachine : StateMachine<PlayerStateMachine>
    {
        [SerializeField] private PlayerInput _playerInput;
        // this is the Transform we want to rotate on the Y axis when changing directions
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Rigidbody2D _rigidbody;

        // this Vector2 can be used on each State to determine any change
        public Vector2 Movement { get; private set; }

        // since our sprite is facing right, we set it to true
        private bool _isFacingRight = true;
        private SpriteRenderer _spriteRenderer;
        
        private void OnEnable()
        {
            _playerInput.MovementEvent += HandleMove;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            Assert.IsNotNull(_spriteRenderer,"The Player must have a Sprite Renderere");
        }

        private void OnDisable()
        {
            _playerInput.MovementEvent -= HandleMove;
        }

        private void HandleMove(Vector2 movement)
        {
            Movement = movement;
            CheckFlipSprite(movement);
        }

        private void CheckFlipSprite(Vector2 velocity)
        {
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
            
            _isFacingRight = !_isFacingRight;
            // _spriteTransform.Rotate(_spriteTransform.rotation.x, 180f, _spriteTransform.rotation.z);
            _spriteRenderer.flipX = _isFacingRight;
        }

        // just  a simple implementation of movement by setting the velocity of the Rigidbody
        public void Move(Vector2 velocity)
        {
            _rigidbody.velocity = velocity;
        }
    }
}