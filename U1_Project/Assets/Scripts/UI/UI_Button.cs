using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
    enum Buttons
    {
        PointButton,
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    private void Start()
    {
        // 리플렉션
        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        //Get<TextMeshProUGUI>((int)Texts.ScoreText).text = "Bind Test";
        GetText((int)Texts.ScoreText).text = "Bind Test";

        GameObject obj = GetImage((int)Images.ItemIcon).gameObject;
        UI_EventHandler evt = obj.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
    }

   
    int _score = 0;

    public void OnButtonClicked()
    {
        _score++;
    }
}
