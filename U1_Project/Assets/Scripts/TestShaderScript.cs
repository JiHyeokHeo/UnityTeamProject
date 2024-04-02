using System.Collections;
using System.Collections.Generic;
using Siccity.GLTFUtility;
using UnityEngine;

public class TestShaderScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject result = Importer.LoadFromFile("Assets/Resources/Prefabs/project_-jax.glb");
        int a = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
