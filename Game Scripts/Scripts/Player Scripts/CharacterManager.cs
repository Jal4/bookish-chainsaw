using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock On Transform")]
    public Transform lockOnTransform;

    [Header("Combat Colliders")]

    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider counterCollider;

    [Header("Combat Flags")]
    public bool canBeCountered;
    public bool canBeParried;
    public bool isParrying;
    public bool isBlocking;

    //Damage is added through animation Event
    public int pendingCriticalDamage;
}
