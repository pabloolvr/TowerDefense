using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Camera _camera;
    private int _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _layerMask = 1 << 6;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow, 999);
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, _layerMask))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log($"Hit {hit.transform.name}");
            }

            //Physics.rayc
            //Raycast
            //Debug.Log(mouseClickPosition);
            //Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
            //transform.position = _camera.ScreenToViewportPoint(Input.mousePosition);
            //Instantiate(cube, mouseClickPosition, Quaternion.identity);
        }
    }
}
