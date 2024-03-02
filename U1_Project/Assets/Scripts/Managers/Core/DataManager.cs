using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager  
{
    public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
    // TODO 추후 늘릴게 필요하다면 늘려라.
    
    /// <summary>
    /// 몬스터를 추가하고 몬스터에 대한 정보가 담겨있는 컨테이너가 있어야할 것같아. 무언가 찾아야하니까 이게 좋겠지?
    /// 그걸로 가장가까운 몬스터한테 접근해야할듯 어떤정보가 필요할까? 이제 딕션어리에 넣어야하는데 지혁이는 어디서 넣는 구간을 만들었지?
    /// </summary>
    public Dictionary<int, Monster> MonsterDic {get; private set;} = new Dictionary<int, Monster>();


    public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
}
