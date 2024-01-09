using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    public static Managers Instance { get { Init(); return s_instance; } }

    MonsterAIManager _monsterAI = new MonsterAIManager();
    ResourceManager _resource = new ResourceManager();

    public static MonsterAIManager MonsterAI { get { return Instance._monsterAI; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _monsterAI.OnUpdate();
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject obj = GameObject.Find("@Managers");
            if(obj == null)
            {
                obj = new GameObject { name = "@Managers" };
                obj.AddComponent<Managers>();
            }

            DontDestroyOnLoad(obj);
            s_instance = obj.GetComponent<Managers>();
        }
    }

}
