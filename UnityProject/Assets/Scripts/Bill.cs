/*
	Copyright (C) 2020 Anarres

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/> 
*/

using UnityEngine;

public class Bill : MonoBehaviour
{

    public Transform laser;
    public float laserSpeed = 20;
    public float laserRenew = 50;

    private bool laserActive = false;
    private Animator animator;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
                audioManager.Play("laserShot", false);
                laser.transform.localPosition = Vector3.zero;
            }
        }
        
    }

    public void onEntranceComplete()
    {
        laserActive = true;
        animator.SetTrigger("Shoot");
        audioManager.Play("laserShot", false);
        laser.gameObject.SetActive(true);
    }
}
