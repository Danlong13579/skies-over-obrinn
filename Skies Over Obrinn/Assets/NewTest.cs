using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTest : MonoBehaviour
{

    [SerializeField] private float forceMod;

    [SerializeField] private int speedMod;

    private Rigidbody rd;

    [SerializeField] private Transform _transform;
    // Start is called before the first frame update

    void Awake()
    {
        rd = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _transform.localPosition = new Vector3(1, 0, 0);
        Debug.Log(_transform.localPosition.x);
        Debug.Log(rd);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            speedMod += 1;
            _transform.localPosition = new Vector3(0, 0, speedMod);
        }
        if (Input.GetKey(KeyCode.S))
        {
            speedMod -= 1;
            _transform.localPosition = new Vector3(0, 0, speedMod);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            forceMod += 0.001f;
            _transform.Rotate(0f,forceMod, 0f);
        }
    }
}
