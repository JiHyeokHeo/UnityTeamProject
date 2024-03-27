using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index > 0)
                name = name.Substring(index + 1);

            GameObject obj = Managers.Pool.GetOriginal(name);
            if (obj != null)
                return obj as T;
        }

        return Resources.Load<T>(path);
    }


    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        GameObject obj = Object.Instantiate(original, parent);
        int index = obj.name.IndexOf("(Clone)");
        if (index > 0)
            obj.name = obj.name.Substring(0, index);

        return obj;
    }

    // 직접 리소스매니저에 접근해서 지우지 마세요! 생명주기랑 관련이 있으므로 건드리면 안됩니다 !
    public void Destroy(GameObject obj)
    {
        if (obj == null)
            return;

        Poolable poolable = obj.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Object.Destroy(obj);
    }
}
