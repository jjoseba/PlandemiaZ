using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatesLaser : MonoBehaviour
{
    public Transform origin;
    private Transform selectedTarget;
    LineRenderer laserLine;

    void Start() {
        laserLine = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(selectedTarget != null){
            laserLine.SetPosition (0, origin.position);
            laserLine.SetPosition (1, selectedTarget.position);
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
