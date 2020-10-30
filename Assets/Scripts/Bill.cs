using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bill : MonoBehaviour
{

    public Transform laser;
    public float laserSpeed = 20;
    public float laserRenew = 50;

    private bool laserActive = false;
    private Animator animator;
    private AudioManager audio;

    void Start()
    {
        audio = FindObjectOfType<AudioManager>();
        laser.transform.localPosition = Vector3.zero;
        laser.gameObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (laserActive)
        {
            laser.transform.localPosition += Vector3.right * Time.deltaTime * laserSpeed;
            if (laser.transform.localPosition.x > laserRenew)
            {
                animator.SetTrigger("Shoot");
                audio.Play("laserShot", false);
                laser.transform.localPosition = Vector3.zero;
            }
        }
        
    }

    public void onEntranceComplete()
    {
        laserActive = true;
        animator.SetTrigger("Shoot");
        audio.Play("laserShot", false);
        laser.gameObject.SetActive(true);
    }
}
