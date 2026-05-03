using System.Collections;
using System.Collections.Generic;
using BerserkPixel.StateMachine;
using Player;
using Player.States;
using UnityEngine;

namespace Enemy.States {
    public class EnemyStateMachine : StateMachine<EnemyStateMachine>
    {
        [SerializeField] PlayerStateMachine _player;
        private LevelManager _levelManager;
        public LevelManager LevelManager { set {_levelManager = value; }}
        public PlayerStateMachine Player { get {return _player; }}
        public override void DamageResponse(int totalHealth)
            {
                SetState(typeof(EnemyRetreatState));
                if (totalHealth <= 0)
                {
                    Destroy(gameObject);
                    _levelManager.OnEnemyKilled(gameObject);
                }
            }
    }
}