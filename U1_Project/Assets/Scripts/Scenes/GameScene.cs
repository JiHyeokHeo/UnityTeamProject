using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Play;

        Managers.UI.ShowSceneUI<UI_Monster_Selector>();

        for (int i = 0; i < 5; i++)
            Managers.Resource.Instantiate("Knight");
    }

    public override void Clear()
    {
        
    }

}
