using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesLaser : MonoBehaviour
{
    public Transform origin;
    public GameObject laserHit;
    private Transform selectedTarget;
    private LineRenderer laserLine;
    private LineRenderer impactLine;
    private bool alreadyHit = false;
    private int layerMask;
    private Vector3? startImpactPosition;

    void Start() {
        laserLine = GetComponent<LineRenderer>();
        layerMask = LayerMask.GetMask("Miguel");
        impactLine = laserHit.GetComponent<LineRenderer>();
        laserHit.SetActive(false);
    }

    void Update()
    {
        if(selectedTarget != null){
            laserLine.SetPosition (0, origin.position);
            laserLine.SetPosition (1, selectedTarget.position);

            startImpactPosition = startImpactPosition ?? selectedTarget.position;
            impactLine.SetPosition(0, startImpactPosition.Value);
            impactLine.SetPosition(1, selectedTarget.position);

            laserHit.SetActive(true);
            laserHit.transform.position = selectedTarget.position;

            if(!alreadyHit){
                RaycastHit hit;
                var direction = selectedTarget.position - origin.position;
                //Debug.DrawRay(origin.position, direction, Color.red); 
                if (Physics.Raycast(origin.position, direction, out hit, Mathf.Infinity, layerMask)) {
                    if (hit.collider) {
                        hit.collider.gameObject.SendMessage("LaserHit");
                        alreadyHit = true;
                        gameObject.SetActive(false);
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
