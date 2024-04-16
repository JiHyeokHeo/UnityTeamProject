using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    float _yPosOffSet = 0.0f;
    [SerializeField]
    Quaternion _rotationOffset = Quaternion.identity;


    public void SetHpBar(float ratio)
    {
        ratio = Mathf.Clamp01(ratio);
        //go.transform.rotation = Camera.main.transform.rotation;
        //_hpBar.localScale = new Vector3(ratio, 1, 1);
    }

    private void Update()
    {
        Transform parent = gameObject.transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y + _yPosOffSet);
        transform.rotation = Camera.main.transform.rotation * _rotationOffset;
    }
}
