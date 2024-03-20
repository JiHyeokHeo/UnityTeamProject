using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    GameObject _player;
    // int <-> GameObject
    //Dictionary<int, GameObject> _env = new Dictionary<int, GameObject>();
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject obj = Managers.Resource.Instantiate(path, parent);
        
        _monsters.Add(obj);

        switch(type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(obj);
                break;
            case Define.WorldObject.Player:
                _player = obj;
                break;
        }

        return obj;
    }

    //public Define.WorldObject GetWorldObjectType(GameObject obj)
    //{
    //    BaseController bc = obj.GetComponent<BaseController>();

    //    if (bc == null)
    //        return Define.WorldObject.Unknown;

    //    return bc.WorldObjectType;
    //}

    //public void DeSpawn(GameObject obj)
    //{
    //    //Define.WorldObject type = GetWorldObjectType(obj);

    //    switch(type)
    //    {
    //        case Define.WorldObject.Monster:
    //            {
    //                if(_monsters.Contains(obj))
    //                    _monsters.Remove(obj);
    //            }
    //            break;
    //        case Define.WorldObject.Player:
    //            {
    //                if (_player == obj)
    //                    _player = null;
    //            }
    //            break;
    //    }

    //    Managers.Resource.Destroy(obj);
    //}

}
