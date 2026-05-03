using BerserkPixel.StateMachine;
using UnityEngine;

namespace Enemy.States
{
    [CreateAssetMenu(menuName = "States/Enemy/Retreat")]
    public class EnemyRetreatState : State<EnemyStateMachine>
    {
        [SerializeField, Range(0.1f, 4f)] float _fleeTime = 1f;
        float _elapsedTime = 0;

        public override void Enter(EnemyStateMachine parent)
        {
            _elapsedTime = 0f;
        }
        public override void Tick(float deltaTime) {
            _elapsedTime += deltaTime;
        }
    
        // similar to FixedUpdate
        public override void FixedTick(float fixedDeltaTime) {
            Vector3 playerPos = _runner.Player.transform.position;
            Vector3 towardPlayer = playerPos - _runner.transform.position;
            _runner.Move(-towardPlayer.normalized * (_runner.Speed * fixedDeltaTime));
        }
    
        // here we put the conditions to change to another state if needed
        public override void ChangeState() {
            if (_elapsedTime >= _fleeTime)
            {
                _runner.SetState(typeof(EnemyApproachState));
            }
        }
    }
}