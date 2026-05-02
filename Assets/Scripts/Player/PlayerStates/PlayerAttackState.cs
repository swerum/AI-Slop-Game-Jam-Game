using System;
using BerserkPixel.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Attack")]
    public class PlayerAttackState : State<PlayerStateMachine>
    {
        CollisionManager _attackCollider;

        [Tooltip("The damage to perform on the target")]
        [SerializeField] float _damage;

        public override void OnStart(PlayerStateMachine parent)
        {
            _attackCollider = parent.GetComponentInChildren<CollisionManager>();
        }
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
        }

        public override void Tick(float deltaTime)
        {
        }

        
        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishAttack)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }
        public override void FixedTick(float fixedDeltaTime)
        {
            if (_attackCollider.CollidingObjects.Count > 0) {
                foreach (var enemy in _attackCollider.CollidingObjects)
                {
                    Debug.Log("Hit an enemy: "+enemy.name);
                } 
            }
        }

        public override void ChangeState()
        {
            if (_runner.Movement.sqrMagnitude != 0)
            {
                _runner.SetState(typeof(PlayerMoveState));
            }
        }
    }
}