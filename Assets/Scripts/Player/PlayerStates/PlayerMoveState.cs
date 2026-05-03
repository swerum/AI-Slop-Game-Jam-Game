using BerserkPixel.StateMachine;
using UnityEngine;

namespace Player.States
{
    [CreateAssetMenu(menuName = "States/Player/Move")]
    public class PlayerMoveState : State<PlayerStateMachine>
    {
        private Vector3 _playerInput;
        private float _speed;

        public override void OnStart(PlayerStateMachine parent)
        {
            _speed = parent.Speed;
        }
        public override void Tick(float deltaTime)
        {
            _playerInput = new Vector3(_runner.Movement.x, 0, _runner.Movement.y);
        }

        public override void FixedTick(float fixedDeltaTime)
        {
            _runner.Move(_playerInput * (_speed * fixedDeltaTime));
        }

        public override void ChangeState()
        {
            if (_runner.AttackPressed)
            {
                _runner.SetState(typeof(PlayerAttackState));
                return;
            }
            if (_runner.RollPressed)
            {
                _runner.SetState(typeof(PlayerRollState));
                return;
            }
            if (_playerInput == Vector3.zero)
            {
                _runner.SetState(typeof(PlayerIdleState));
            }
        }
    }
}