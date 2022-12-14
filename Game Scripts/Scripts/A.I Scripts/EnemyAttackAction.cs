using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "A.I Actions / AttackActions")]
public class EnemyAttackAction : EnemyActions
{
    public bool canCombo;

    public EnemyAttackAction comboAction;

    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float minimumDistanceNeededToAttack = 0;
    public float maximumDistanceNeededToAttack = 3;
}
