using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    public static movement Instance;

    private Rigidbody2D rb;

    public float forceAmount = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    } 

    // Start is called before the first frame update
    void Start()
    {
        //rb = <Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce((Vector3.up * forceAmount));
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce((Vector3.down * forceAmount));
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce((Vector3.left * forceAmount));
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce((Vector3.right * forceAmount));
        }

        rb.velocity *= 0.4f;
    }
}
