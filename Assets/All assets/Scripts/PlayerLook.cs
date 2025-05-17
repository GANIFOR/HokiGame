using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{


    public Transform Camera;
    void Shoot()
    {
        Ray ray = new Ray(Camera.position, Camera.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10))
        {
            
            if (hit.transform.tag == "Shootable")
            {
                hit.rigidbody.AddForceAtPosition(ray.direction * 30, hit.point, ForceMode.Impulse);
            }
        }
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        { 
            Shoot(); 
        }
    }
}
