using BerserkPixel.StateMachine;
using Player;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Attack")]
    public class EnemyAttackState : State<EnemyStateMachine>
    {

        public override void OnStart(EnemyStateMachine parent)
        {
            base.OnStart(parent);
        }
        public override void Tick(float deltaTime) {

        }
    
        // similar to FixedUpdate
        public override void FixedTick(float fixedDeltaTime) {

        }
    
        // here we put the conditions to change to another state if needed
        public override void ChangeState() {

        }
        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishAttack)
            {
                _runner.SetState(typeof(EnemyApproachState));
            }
        }
    }
}