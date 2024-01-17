using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab ==null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        GameObject obj = Object.Instantiate(prefab, parent);
        int index = obj.name.IndexOf("(Clone)");
        if (index > 0)
            obj.name = obj.name.Substring(0, index);

        return obj;
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null)
            return;

        Object.Destroy(obj);
    }
}
