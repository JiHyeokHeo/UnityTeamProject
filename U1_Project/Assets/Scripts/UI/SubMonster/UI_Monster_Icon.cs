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

        // TODO �̹��� ���� �ʿ� // ���� Ȯ���� �̹��� �������� ���� 
        Get<GameObject>((int)GameObjects.Monster_Image).BindEvent((PointerEventData) => { Debug.Log($"��ȯ Ŭ�� !"); });

    }

    public void SetImage(/* �̹��� �Ķ���� */)
    {
        
    }
}
