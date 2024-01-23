using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension 
{
    public static T GetOrAddComponent<T>(this GameObject obj) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(obj);
    }

    public static void BindEvent(this GameObject obj, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindEvent(obj, action, type);
    }

    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    }
}
