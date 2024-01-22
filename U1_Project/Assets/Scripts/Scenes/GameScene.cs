using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    Coroutine co;

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Play;

        Managers.UI.ShowSceneUI<UI_Monster_Selector>();

        Dictionary<int, Stat> dict = Managers.Data.StatDict;
        
        //co = StartCoroutine("ExplodeAfterSeconds", 4.0f);
    }

    IEnumerator ExplodeAfterSeconds(float seconds)
    {
        Debug.Log("Explode Enter");
        yield return new WaitForSeconds(seconds);
        Debug.Log("Explode Execute!!!");
    }

    public override void Clear()
    {
        
    }

}
