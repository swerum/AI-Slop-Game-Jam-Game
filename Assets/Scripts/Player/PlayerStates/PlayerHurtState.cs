using BerserkPixel.StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Hurt")]
    public class PlayerHurtState : PlayerMoveState
    {
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            parent.SetInvincible();
        }
        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishHurt)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }
    }
}