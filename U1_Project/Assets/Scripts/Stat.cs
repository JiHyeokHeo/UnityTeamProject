using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    protected int _level;
    protected int _hp;
    protected int _maxHp;
    protected int _attack;
    protected int _defense;
    protected float _moveSpeed;

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp{ get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defense { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        _level = 1;

        //Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //Data.Stat stat = dict[1];

        //_hp = stat.hp;
    }

    public virtual void OnAttacked(Stat attacker)
    {
        int dmg = Mathf.Max(0, attacker.Attack - Defense);
        Hp -= dmg;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }
    }

    protected virtual void OnDead(Stat attacker)
    {
        Managers.Game.DeSpawn(gameObject);
    }
}
