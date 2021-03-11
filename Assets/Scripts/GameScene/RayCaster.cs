using System;
using System.Linq;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    /*private Camera mainCamera;
    private Vector3 currentPosition = Vector3.zero;*/

    int layerMask = 1 << 11;

    void Start()
    {
        //mainCamera = Camera.main;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            //レイが当たった位置を得るよ
            Vector3 pos = hit.point;
            pos = new Vector3(pos.x, 2.46f, pos.z);
            //Debug.Log(pos);
            this.transform.position = pos;
        }
        /*var distance = Vector3.Distance(mainCamera.transform.position, raycastHit.origin);
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);

        currentPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        Debug.Log(currentPosition);
        this.transform.position = currentPosition;
        this.transform.position = new Vector3(this.transform.position.x, 2.46f, this.transform.position.z);
        currentPosition.y = 0;*/
    }

    /*void OnDrawGizmos()
    {
        if (currentPosition != Vector3.zero)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(currentPosition, 0.5f);
        }
    }*/
}