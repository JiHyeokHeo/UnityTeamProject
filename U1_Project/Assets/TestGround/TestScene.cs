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

        //maager 이것때문에 Select UI가 보이네 UI하단에 흰색, 지혁이 6칸으로 할 모양이네, 당장안필요해
        //Managers.UI.ShowSceneUI<UI_Monster_Selector>();

        //Managers에는 하위 매니저가있는데 그중 Data에 있는 StatDcit을 딕셔너리에 넣네 이것도 내가 쓸필요는 없는 딕셔너리인듯?
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        //stat
        //public string name;
        //public int level;
        //public int hp;
        //public int mp;
        //public int attack;
        //public int attackRange;


    #region 패스파인딩
    //Managers.Resource.Instantiate("Seeker");
    //Managers.Resource.Instantiate("Target");
    //Managers.Resource.Instantiate("Map/Astar");
    #endregion

    //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
    }

    private void Start()
    {


        Managers.Pool.Init();
        //Debug.Log("ㅇ");

        GameObject ob = Managers.Resource.Load<GameObject>("TestPref/TestMob");
        Debug.Log(ob);
        //몬스터 풀을 만들어야해 //풀매니저보다 더 빨리호출됨
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
