using BerserkPixel.StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Death")]
    public class PlayerDeathState : State<PlayerStateMachine>
    {
        public override void Enter(PlayerStateMachine parent)
        {
            base.Enter(parent);
            _runner.PlayerInput.SetInputType(Input.InputType.UI);
            _runner.IsInvincible = true;
        }

        public override void Tick(float deltaTime)
        {
        }

        public override void FixedTick(float fixedDeltaTime)
        {
        }

        public override void ChangeState()
        {
        }
        public override void AnimationTriggerEvent(AnimationTriggerType triggerType)
        {
            base.AnimationTriggerEvent(triggerType);

            if (triggerType == AnimationTriggerType.FinishDeath)
            {
                _runner.GameManager.OpenMenuFromGameplay(GameState.GameOver);
            }
        }
    }
}