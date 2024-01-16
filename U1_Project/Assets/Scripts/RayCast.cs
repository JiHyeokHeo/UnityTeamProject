using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌 :  { collision.gameObject.name} !");
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 look = transform.TransformDirection(Vector3.forward);
        //Debug.DrawRay(transform.position + Vector3.up * 0.25f, look * 10, Color.red);

        //RaycastHit hit;
        //if (Physics.Raycast(transform.position + Vector3.up * 0.25f, look, out hit, 10))
        //{
        //    Debug.Log($" 레이케스트 충돌 {hit.collider.gameObject.name} ! ");
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            //Vector3 dir = mousePos - Camera.main.transform.position;
            //dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            int  mask = (1 << 7) | (1 << 6);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                Debug.Log($"레이캐스트 카메라 { hit.collider.gameObject.name}");
            }
        }
    }
}
