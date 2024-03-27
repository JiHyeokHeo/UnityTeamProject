using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrpt : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject _go;
    Action _action;
    bool isPicked = false;
    void Start()
    {
        _go = this.gameObject;
        _action += TestClicked;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"MousePos : {Input.mousePosition}, ScreenWidth : {Screen.width}, ScreenHeight : {Screen.height}");
        Debug.Log(_go.transform.position);
        if (Input.GetMouseButtonDown(0))
            _action.Invoke();

        if (isPicked)
            FollowObjectByMousePointer();
        
    }

    void FollowObjectByMousePointer()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _go.transform.position.y;
        transform.position = Camera.main.ScreenToViewportPoint(mousePos);
    }
    void TestClicked()
    {
        GameObject go = RayCast.FindFirstObjectByType<GameObject>();
        if (_go = go)
            isPicked = true;
    }
}
