using Player;
using UnityEngine;

[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerTakesDamage : TakesDamage<PlayerStateMachine>
{
}
