using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedParallax : MonoBehaviour
{

    public Transform target;
    public float distance;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + offset;
    }
}
