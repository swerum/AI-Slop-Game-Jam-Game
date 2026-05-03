using System;
using BerserkPixel.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        [SerializeField, Range(0, 1)] private float _movementSlowdown = 0.5f;

        [Tooltip("The damage to perform on the target")]
        [SerializeField] float _damage;
        private float _attackMovementSpeed;
        CollisionManager _attackCollider;
        private Vector3 _playerInput;

        public override void OnStart(PlayerStateMachine parent)
        {
            _attackCollider = parent.GetComponentInChildren<CollisionManager>();
            _attackMovementSpeed = parent.Speed * _movementSlowdown;
        }
        public override void Tick(float deltaTime)
        {
            _playerInput = new Vector3(_runner.Movement.x, 0, _runner.Movement.y);
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            _runner.Move(_playerInput * (_attackMovementSpeed * fixedDeltaTime));

            if (_attackCollider.CollidingObjects.Count > 0) {
                foreach (var enemy in _attackCollider.CollidingObjects)
                {
                    Debug.Log("Hit an enemy: "+enemy.name);
                } 
            }
        }

        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
        }
        
        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishAttack)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }
        public override void ChangeState()
        {
        }
    }
}