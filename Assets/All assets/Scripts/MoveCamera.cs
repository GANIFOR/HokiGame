using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform CameraPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = CameraPosition.position;
    }
}
