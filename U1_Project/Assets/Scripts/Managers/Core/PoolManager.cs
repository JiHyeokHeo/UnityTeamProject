using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PoolManager 
{
    #region Pool
    class Pool
    {
        public GameObject Original { get; private set; }
        public GameObject Root { get; set; }

        Queue<Poolable> _poolQueue = new Queue<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject();
            Root.name = $"{Original.name}_Root";

            for(int i =0; i< count; i++)
                Push(Create());
        }

        Poolable Create()
        {
            GameObject obj = Object.Instantiate<GameObject>(Original);
            obj.name = Original.name;
            return obj.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root.transform;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;
            
            _poolQueue.Enqueue(poolable);
        }

        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolQueue.Count > 0)
                poolable = _poolQueue.Dequeue();
            else
                poolable = Create();

            poolable.gameObject.SetActive(true);

            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.transform.parent = parent;
            poolable.IsUsing = true;

            return poolable;
        }
    }
    #endregion
    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();
    GameObject _root;

    public void Init()
    {
        if (_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" };
            Object.DontDestroyOnLoad(_root);
        }    
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.transform.parent = _root.transform;

        _pool.Add(original.name, pool);
    }

    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        if(_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }

        _pool[name].Push(poolable);
    }


    public Poolable Pop(GameObject original, Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;

        return _pool[name].Original;
    }

    public void Clear()
    {
        foreach (Transform child in _root.transform)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }

}
