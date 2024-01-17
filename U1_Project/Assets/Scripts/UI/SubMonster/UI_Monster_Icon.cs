using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Monster_Icon : UI_Base
{
    enum GameObjects
    {
        Monster_Image
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));

        // TODO 이미지 변경 필요 // 일정 확률로 이미지 나오도록 설정 
        Get<GameObject>((int)GameObjects.Monster_Image).BindEvent((PointerEventData) => { Debug.Log($"소환 클릭 !"); });

    }

    public void SetImage(/* 이미지 파라미터 */)
    {
        
    }
}
