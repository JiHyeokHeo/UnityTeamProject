using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompareGameObjectPos : IComparer<GameObject>
{
    public int data;

    public CompareGameObjectPos(int data) //������
    {
        this.data = data;
    }

    public int Compare(GameObject x, GameObject y)
    {
        throw new NotImplementedException();
    }

    //IComparable ��� �� CompareTo �޼��带 �����ؾ� �Ѵ�.
    //this�� obj�� ������ ��, this�� ���� ������ -1�� ��ȯ�ϰ�, ũ�� 1�� ��ȯ�Ѵ�.
    //Sort()���� CompareTo()�� ��ȯ���� �������� �����ϴµ� �̸� �پ��ϰ� ���� �����ϴ�.
    //�Ʒ� �ڵ�� data�� �������� �����ϴ� �ڵ��̴�.
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
