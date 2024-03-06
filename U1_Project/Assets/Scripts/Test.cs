using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompareGameObjectPos : IComparer<GameObject>
{
    public int data;

    public CompareGameObjectPos(int data) //생성자
    {
        this.data = data;
    }

    public int Compare(GameObject x, GameObject y)
    {
        throw new NotImplementedException();
    }

    //IComparable 상속 시 CompareTo 메서드를 정의해야 한다.
    //this와 obj를 비교했을 때, this의 값이 작으면 -1을 반환하고, 크면 1을 반환한다.
    //Sort()에서 CompareTo()의 반환값을 기준으로 정렬하는데 이를 다양하게 응용 가능하다.
    //아래 코드는 data을 조건으로 정렬하는 코드이다.
    public int CompareTo(object obj)
    {
        if (data < (obj as CompareGameObjectPos).data)
            return -1;
        else
            return 1;
    }
}

public class Test : MonoBehaviour
{
    List<GameObject> _gameObjects = new List<GameObject>();
    GameObject obj;
    void Start()
    {
        obj = Managers.Resource.Instantiate("Knight");
        //Managers.Resource.Destroy(obj);
        Destroy(obj, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
