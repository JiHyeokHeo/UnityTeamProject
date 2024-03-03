using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Play;

        //maager �̰Ͷ����� Select UI�� ���̳� UI�ϴܿ� ���, ������ 6ĭ���� �� ����̳�, ������ʿ���
        //Managers.UI.ShowSceneUI<UI_Monster_Selector>();

        //Managers���� ���� �Ŵ������ִµ� ���� Data�� �ִ� StatDcit�� ��ųʸ��� �ֳ� �̰͵� ���� ���ʿ�� ���� ��ųʸ��ε�?
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //stat
        //public string name;
        //public int level;
        //public int hp;
        //public int mp;
        //public int attack;
        //public int attackRange;


    #region �н����ε�
    //Managers.Resource.Instantiate("Seeker");
    //Managers.Resource.Instantiate("Target");
    //Managers.Resource.Instantiate("Map/Astar");
    #endregion

    //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
    }

    private void Start()
    {


        Managers.Pool.Init();
        //Debug.Log("��");

        GameObject ob = Managers.Resource.Load<GameObject>("TestPref/TestMob");
        Debug.Log(ob);
        //���� Ǯ�� �������� //Ǯ�Ŵ������� �� ����ȣ���
        //Managers.Pool.CreatePool(Managers.Resource.Load<GameObject>("Assets/TestGround/TestMob.prefab"));
        //
    }


    IEnumerator Tset(float seconds)
    {
        Debug.Log(" Enter");
        yield return new WaitForSeconds(seconds);
    }

    public override void Clear()
    {
        
    }

}
