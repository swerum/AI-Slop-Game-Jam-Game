using System;
using BerserkPixel.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        [SerializeField, Range(10, 100)] private float _attackMovementSpeed = 25;

        [Tooltip("The damage to perform on the target")]
        [SerializeField] float _damage;
        CollisionManager _attackCollider;
        private Vector3 _playerInput;

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

        public override void OnStart(PlayerStateMachine parent)
        {
            _attackCollider = parent.GetComponentInChildren<CollisionManager>();
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