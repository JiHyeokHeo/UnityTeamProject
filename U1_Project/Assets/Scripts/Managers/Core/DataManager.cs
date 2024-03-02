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
    // TODO ���� �ø��� �ʿ��ϴٸ� �÷���.
    
    /// <summary>
    /// ���͸� �߰��ϰ� ���Ϳ� ���� ������ ����ִ� �����̳ʰ� �־���� �Ͱ���. ���� ã�ƾ��ϴϱ� �̰� ������?
    /// �װɷ� ���尡��� �������� �����ؾ��ҵ� ������� �ʿ��ұ�? ���� ��Ǿ�� �־���ϴµ� �����̴� ��� �ִ� ������ �������?
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
