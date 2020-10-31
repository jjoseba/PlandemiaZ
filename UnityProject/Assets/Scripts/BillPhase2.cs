using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillPhase2 : MonoBehaviour
{
    public Transform centerTarget;
    public Transform leftTarget;
    public Transform rightTarget;
    public float coolDownTime = 7f;
    public Transform gun;
    public Transform shootingPoint;
    public GameObject laser;
    
    float gunRotationAngle = 10f;
    Quaternion gunStartingAngle;
    float nextShootTime;
    int selectedTarget;
    float selectedAngle;
    Animator animator;

    void Start () {
        nextShootTime = coolDownTime;
        gunStartingAngle = gun.rotation;
        animator = GetComponent<Animator>(); 
    }
    
    void Update() {
        
         if(CanShoot()){
            StartCoroutine(BeginShooting()); 
            nextShootTime = coolDownTime;
         } else {
             nextShootTime -= Time.deltaTime;
         }
    }

    IEnumerator BeginShooting(){
        StartCoroutine(Aim());

        yield return new WaitForSeconds(5.0f);

        StartCoroutine(Shoot());
        
        yield return new WaitForSeconds(3.0f);
    }

    private IEnumerator Aim(){
        selectedTarget = Random.Range(-1,2);
        selectedAngle = gunRotationAngle * selectedTarget;
        StartCoroutine(RotateGun(new Vector3(0f, -selectedAngle, 0f), 0.5f));
        yield return new WaitForSeconds(0.5f);

        SwithcAimingTargetVisibility(selectedTarget);

        yield return null;

    }

    private IEnumerator Shoot() {
        animator.SetTrigger("Shoot");
        var laserInstance = Instantiate(laser, shootingPoint.position, shootingPoint.rotation) as GameObject;
        laserInstance.SendMessage("SelectTarget", selectedTarget);
        nextShootTime = coolDownTime;
        
        SwithcAimingTargetVisibility(selectedTarget);

        yield return new WaitForSeconds(3.0f);

        Destroy(laserInstance);
        StartCoroutine(RotateGun(new Vector3(0f, selectedAngle, 0f), 0.5f));
        yield return new WaitForSeconds(0.5f);
    }

    private bool CanShoot() {
        return nextShootTime <= 0;
    }

    private void SwithcAimingTargetVisibility(int target) {
        switch(target){
            case -1: leftTarget.gameObject.SetActive(!leftTarget.gameObject.activeSelf); break;
            case 0: centerTarget.gameObject.SetActive(!centerTarget.gameObject.activeSelf); break;
            case 1: rightTarget.gameObject.SetActive(!rightTarget.gameObject.activeSelf); break;
        }
    }

    IEnumerator RotateGun(Vector3 angle, float duration)
    {
        Quaternion newRotation = Quaternion.Euler( angle ) * gunStartingAngle;
        for( float t = 0 ; t < duration ; t+= Time.deltaTime )
        {
            gun.transform.rotation = Quaternion.Lerp(gunStartingAngle, newRotation, t / duration );
            yield return null;
        }
        gunStartingAngle = newRotation;

        yield return null;
    }
}
