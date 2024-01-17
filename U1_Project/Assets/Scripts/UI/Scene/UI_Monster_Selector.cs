using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Monster_Selector : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // 실제 인벤토리 정보를 참고해서 넣어줘야함
        for (int i = 0; i < 6; i++)
        {
            GameObject item = Managers.Resource.Instantiate("UI/Scene/UI_Monster_Icon");
            item.transform.SetParent(gridPanel.transform);

            UI_Monster_Selector_Monster monster = item.GetOrAddComponent<UI_Monster_Selector_Monster>();
        }
    }

    
}
