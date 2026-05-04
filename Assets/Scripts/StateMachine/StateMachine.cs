using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace BerserkPixel.StateMachine
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class StateMachine<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private List<State<T>> _states;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _spriteTransform;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private HealthBar _healthBar;
        [Header("Game Design")]
        [SerializeField, Range(10, 100)] private float _speed = 50;
        [SerializeField, Range(0, 1)] private float _invincibilityTime;
        [SerializeField, Range(1, 10)]  private int _flashesDuringInvincibility = 7;
        [SerializeField, Range(0, 500)] private int _maxHealth;
        [SerializeField] private bool _facesRightByDefault;
        public float Speed {get {return _speed; }}

        [Header("DEBUG")] 
        [SerializeField] private bool _debug = true;

        private State<T> _activeState;
        private bool _isInvincible = false;
        public bool IsInvincible { get { return _isInvincible; } set { _isInvincible = value; }}
        // since our sprite is facing right, we set it to true
        private bool _isFacingRight = true;
        public bool IsFacingRight { get { return _isFacingRight;}}
        private SpriteRenderer _spriteRenderer;
        private Transform _attackObject;
        

        private T _parent;

        protected virtual void Awake()
        {
            _parent = GetComponent<T>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _attackObject = GetComponentInChildren<DoesDamage>().transform;
            _healthBar.Initialize(_maxHealth);
        }

        protected virtual void Start()
        {
            if (_states.Count <= 0) return;
            foreach (var state in _states)
            {
                state.OnStart(_parent);
            }
            SetState(_states[0]);
        }

        public void SetState(State<T> newStateType)
        {
            _activeState?.Exit();
            _activeState = newStateType;
            _activeState?.Enter(_parent);
        }

        public void SetState(Type newStateType)
        {
            var newState = _states.FirstOrDefault(s => s.GetType() == newStateType);
            if (newState)
            {
                if (newState.StateAnimation != null) {
                    _animator.Play(newState.StateAnimation);
                }
                SetState(newState);
            }
        }

        protected virtual void Update()
        {
            _activeState?.Tick(Time.deltaTime);
            _activeState?.ChangeState();
        }

        /// <summary>
        /// Can be called from the Animation Timeline. This will propagate the AnimationTriggerType
        /// to the current active state.
        /// </summary>
        /// <param name="triggerType"></param>
        private void SetAnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            _activeState?.AnimationTriggerEvent(triggerType);
        }

        private void FixedUpdate()
        {
            _activeState?.FixedTick(Time.fixedDeltaTime);
        }

        private void OnGUI()
        {
            if (!_debug) return;

            var content = _activeState != null ? _activeState.name : "(no active state)";
            GUILayout.Label($"<color='black'><size=40>{content}</size></color>");
        }

        public void SetInvincible()
        {
            _isInvincible = true;
            StartCoroutine(UnsetInvincible());

            IEnumerator UnsetInvincible()
            {
                bool isVisible = true;
                for (int i = 0; i < _flashesDuringInvincibility; i++)
                {
                    isVisible = !isVisible;
                    _spriteRenderer.enabled = isVisible;
                    yield return new WaitForSeconds(_invincibilityTime/_flashesDuringInvincibility);
                }
                _spriteRenderer.enabled = true;
                _isInvincible = false;
            }
        }

        // just  a simple implementation of movement by setting the velocity of the Rigidbody
        public void Move(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
            if ((!(velocity.x > 0f) || _isFacingRight) && (!(velocity.x < 0f) || !_isFacingRight)) return;
            
            _isFacingRight = !_isFacingRight;
            _attackObject.Rotate(_spriteTransform.rotation.x, 180f, _spriteTransform.rotation.z);
            _spriteRenderer.flipX = _facesRightByDefault ? !_isFacingRight : _isFacingRight;
        }
        public virtual void Hit(int damage) {
            if (_isInvincible) return;
            int totalHealth = _healthBar.Hurt(damage);
            DamageResponse(totalHealth);
        }
        public abstract void DamageResponse(int totalHealth);

    }
}