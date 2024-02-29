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
    /// ��󿡰� �ٰ����� ���
    /// </summary>
    public virtual void UnitMove(Transform pos) { }

    /// <summary>
    /// ���� ã�� ���
    /// </summary>
    public virtual void FindEnemy() { }
}
