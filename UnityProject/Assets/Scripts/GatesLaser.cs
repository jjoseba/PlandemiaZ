using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesLaser : MonoBehaviour
{
    public Transform origin;
    private Transform selectedTarget;
    private LineRenderer laserLine;
    private bool alreadyHit = false;
    private int layerMask;

    void Start() {
        laserLine = GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Miguel");
    }

    void Update()
    {
        if(selectedTarget != null){
            laserLine.SetPosition (0, origin.position);
            laserLine.SetPosition (1, selectedTarget.position);

            if(!alreadyHit){
                RaycastHit hit;
                var direction = selectedTarget.position - origin.position;
                if (Physics.Raycast(origin.position, direction, out hit, Mathf.Infinity, layerMask)) {
                    if (hit.collider) {
                        hit.collider.gameObject.SendMessage("LaserHit");
                        alreadyHit = true;
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    void SelectTarget(int targetId) {
        switch(targetId){
            case -1: selectedTarget = GameObject.FindGameObjectWithTag("LeftTarget").transform;    break;
            case 0:  selectedTarget = GameObject.FindGameObjectWithTag("CenterTarget").transform;  break;
            case 1:  selectedTarget = GameObject.FindGameObjectWithTag("RightTarget").transform;   break;
        }
    }

}
