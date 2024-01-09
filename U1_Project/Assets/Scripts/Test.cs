using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
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
