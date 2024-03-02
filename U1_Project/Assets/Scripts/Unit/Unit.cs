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
    /// 컨테이너 돌면서 가장 가까운 적을 찾는 기능 //매개변수로 매니저의 컨테이너를 찾아야함
    /// </summary>
    public virtual Champion FindTarget() {
        Champion temp = new Champion();
        
        return temp;
    }

    /// <summary>
    /// 기본공격
    /// </summary>
    public virtual void DefaultAttack() {
        // 적을 찾음
        Champion target = FindTarget();

        // 


    }
}
