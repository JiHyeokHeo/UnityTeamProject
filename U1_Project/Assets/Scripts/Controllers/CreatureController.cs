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
        GameObject go = Managers.Resource.Instantiate("UI/WorldSpace/UI_HPBar", transform);


        go.transform.localPosition = transform.localPosition + new Vector3(0, 400.0f, 0);
        go.transform.rotation = Camera.main.transform.rotation;
        
        // 사이즈 변환 
        // 0.5f hp 체력바 사이즈값 하드코딩
        GameObject parentGameObject = go.transform.parent.gameObject;
        float scaleFactor = 0.05f / parentGameObject.transform.localScale.x; 
        go.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

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
