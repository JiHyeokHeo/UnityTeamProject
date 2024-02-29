using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected int numCode;

    protected float hp;
    protected float hp_max;
    protected float dmg_physic;
    protected float dmg_magic;
    protected float defense_physic;
    protected float defense_magic;
    protected float speed_attack;
    protected float speed_move;

    protected string unitName;

    /// <summary>
    /// 대상에게 다가가능 기능
    /// </summary>
    public virtual void UnitMove(Transform pos) { }

    /// <summary>
    /// 적을 찾는 기능
    /// </summary>
    public virtual void FindEnemy() { }
}
