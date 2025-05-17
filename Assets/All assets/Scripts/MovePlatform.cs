using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float speed = 2f;

    public float amplitude = 5f;


    private Vector3 startPosition;

    void Start()
    {
   
        startPosition = transform.position;
    }
    

    void Update()
    {

        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPosition + new Vector3(offset, 0f, 0f);
        
    }

    
}
