using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Channeling,
    }

    protected override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        Managers.Input.KeyAction -= OnKeyboard;
        Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
        //Managers.Resource.Instantiate("UI/UI_Button");

        //Managers.UI.ShowPopupUI<UI_Button>();

        //Managers.UI.ClosePopupUI(button);
    }

    protected override void UpdateDie()
    {
        // �ƹ��͵� ����
    }

    protected override void UpdateMoving()
    {

    }

    protected override void UpdateIdle()
    {

    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;

        }
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt == Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);


        RaycastHit hit;
        //int mask = (1 << 8);
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Ground")))
        {
            // TODO
            // Ư�� �׸��忡 ���� ��ġ�ǵ��� ����
        }
    }
}
