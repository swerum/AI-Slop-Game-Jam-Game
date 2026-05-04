using BerserkPixel.StateMachine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Retreat")]
    public class EnemyRetreatState : State<EnemyStateMachine>
    {
        [SerializeField, Range(0.1f, 4f)] float _retreatTime = 1f;
        [SerializeField, Range(0,100)] float _retreatSpeed =  40;
        float _elapsedTime = 0;

        public override void Enter(EnemyStateMachine parent)
        {
            base.Enter(parent);
            _elapsedTime = 0f;
        }
        public override void Tick(float deltaTime) {
            _elapsedTime += deltaTime;
        }
    
        // similar to FixedUpdate
        public override void FixedTick(float fixedDeltaTime) {
            Vector3 towardPlayer =_runner.GetVectorToPlayer();
            _runner.Move(-towardPlayer.normalized * (_retreatSpeed * fixedDeltaTime));
        }
    
        // here we put the conditions to change to another state if needed
        public override void ChangeState() {
            if (_elapsedTime >= _retreatTime)
            {
                _runner.SetState(typeof(EnemyApproachState));
            }
        }
    }
}