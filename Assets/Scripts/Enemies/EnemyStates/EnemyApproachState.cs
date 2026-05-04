using BerserkPixel.StateMachine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Approach")]
    public class EnemyApproachState : State<EnemyStateMachine>
    {
        [SerializeField] float _targetDistanceFromPlayer;

        public override void OnStart(EnemyStateMachine parent)
        {
            base.OnStart(parent);
        }
        public override void Tick(float deltaTime) {
        }
    
        // similar to FixedUpdate
        public override void FixedTick(float fixedDeltaTime) {
            Vector3 towardPlayer =_runner.GetVectorToPlayer();
            if (towardPlayer.magnitude <= _targetDistanceFromPlayer)
            {
                bool hasAttackState = _runner.SetState(typeof(EnemyAttackState));
                if (!hasAttackState) {_runner.SetState(typeof(EnemyRetreatState));}
                return;
            }
            _runner.Move(towardPlayer.normalized * (_runner.Speed * fixedDeltaTime));

        }
    
        // here we put the conditions to change to another state if needed
        public override void ChangeState() {
        }
    }
}