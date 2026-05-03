using Enemy.States;
using Player;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine))]
public class EnemyTakesDamage : TakesDamage<EnemyStateMachine>
{
}
