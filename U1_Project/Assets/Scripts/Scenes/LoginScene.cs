using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        for (int i = 0; i < 2; i++)
            Managers.Resource.Instantiate("Knight");


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Play);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}
