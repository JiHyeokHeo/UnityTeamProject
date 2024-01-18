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
    }

    public override void Clear()
    {
        
    }

}
