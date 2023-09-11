using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonMovement : MonoBehaviour
{
    public Vector2 targetPos;

    public float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
            targetPos, speed * Time.deltaTime);
    }
}
