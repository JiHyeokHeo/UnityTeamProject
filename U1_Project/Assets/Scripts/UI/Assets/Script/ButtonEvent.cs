using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    // Hierarchy 에서 3D gameobject의 child와 cube 찾고 cube를 해당 object의 로컬 벡터 방향으로 transform, rotate 시키기
    [SerializeField]
    GameObject cube;
    GameObject dir;
    public Button button;
    
    void Start()
    {
        
    }
    
    public void OnButtonClicked()
    {
        findObject();
        Debug.Log("Clicked");
        //cube.transform.rotation = Quaternion.Slerp(0, 0, 1);
        Vector3 cubePos = cube.transform.localPosition;
        Vector3 dirPos = dir.transform.localPosition;   // reference 문제는 어떻게?
        cubePos = dirPos;
        transform.position = cubePos;
    }
    void findObject()
    {
        cube = GameObject.Find("Cube");
        dir = GameObject.Find("Plane");
    }
    //void setPosition()
    //{

    //}

}
