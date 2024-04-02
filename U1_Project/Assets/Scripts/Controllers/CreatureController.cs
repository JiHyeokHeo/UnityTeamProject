using Google.Protobuf.Protocol;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CreatureController : BaseController
{
    HpBar _hpBar;

    public override StatInfo Stat
    {
        get { return base.Stat; }
        set { base.Stat = value; UpdateHpBar(); }
    }

    public override int Hp
    {
        get { return Stat.Hp; }
        set { base.Hp = value; UpdateHpBar(); }
    }

    protected void AddHpBar()
    {
        GameObject go = Managers.Resource.Instantiate("UI/HpBar", transform);
        go.transform.localPosition = new Vector3(0, 1.3f, 0);
        go.transform.rotation = Camera.main.transform.rotation;
        go.name = "HpBar";
        _hpBar = go.GetComponent<HpBar>();
        UpdateHpBar();
    }

    void UpdateHpBar()
    {
        if (_hpBar == null)
            return;

        float ratio = 0.0f;
        if (Stat.MaxHp > 0)
            ratio = ((float)Hp / Stat.MaxHp);

        _hpBar.SetHpBar(ratio);
    }

    protected override void Init()
    {
        base.Init();
        AddHpBar();
    }

    public virtual void OnDamaged()
    {

    }

    public virtual void OnDead()
    {
        State = CreatureState.Dead;

    }
    public virtual void UseSkill(int skillId)
    {

    }
}
