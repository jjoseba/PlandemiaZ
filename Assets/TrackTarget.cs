using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour
{

    public Transform target;
    public bool trackX, trackY, trackZ = true;
    public bool trackRotation = false;
    private Vector3 offset;
    private bool fetched = false;

    // Start is called before the first frame update
    void Start()
    {
        fetchPosition();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            trackX ? target.position.x + offset.x : transform.position.x,
            trackY ? target.position.y + offset.y : transform.position.y,
            trackZ ? target.position.z + offset.z : transform.position.z
        );
        if (trackRotation)
        {
            transform.rotation = target.rotation;
        }
    }

    public void fetchPosition()
    {
        Debug.Log(transform.gameObject.name + ": Fetch position!");
        if (fetched) return;
        offset = transform.position - target.position;
        fetched = true;
    }
}
