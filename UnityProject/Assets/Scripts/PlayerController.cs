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

public class PlayerController : MonoBehaviour
{

    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed = 1;
    public float lateralSpeed = 2;
    public float jumpForce = 10;
    public float gravity = 20;
    public float speedIncrease = 0.5f;

    private int desiredLane = 1;
    private int numLanes = 3;
    public float laneDistance = 4; //Distance between running lanes

    public GameObject sprite;
    public LevelManager level;
    public GameObject dropBleach;

    private float leftPosition;

    const int DIR_LEFT = -1;
    const int DIR_RIGHT = 1;

    private bool alive;
    private Animator animator;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        direction.z = forwardSpeed;
        leftPosition = transform.position.x - laneDistance * Mathf.FloorToInt(numLanes/2);
        alive = true;
    }

    private void Update()
    {
        if (direction.z > 0)
        {
            direction.z = forwardSpeed + LevelManager.level * speedIncrease;
        }

        if (controller.isGrounded)
        {
            
            if (alive && (Input.GetKeyDown(KeyCode.UpArrow) || SwipeController.swipeUp)) { Jump(); }
        }
        else
        {
            direction.y -= gravity * Time.deltaTime;
        }

        if (alive)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeController.swipeRight) { ChangeLane(DIR_LEFT); }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeController.swipeLeft) { ChangeLane(DIR_RIGHT); }
            float targetPosition = leftPosition + desiredLane * laneDistance;
            if ((direction.x > 0 && targetPosition < transform.position.x)
                || (direction.x < 0 && targetPosition > transform.position.x))
            {
                direction.x = 0;
                transform.position = new Vector3(targetPosition, transform.position.y, transform.position.z);
            }
        }
        
        
    }

    private void ChangeLane(int dir)
    {
        int targetLane = Mathf.Clamp(desiredLane + dir, 0, numLanes - 1);
        if (targetLane != desiredLane)
        {
            desiredLane = targetLane;
            direction.x = dir * lateralSpeed;
        }
    }

    private void Jump()
    {
        direction.y = jumpForce;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacles")
        {
            
            if (hit.gameObject.GetComponent<Rigidbody>() != null)
            {
                Rigidbody rigid = hit.gameObject.GetComponent<Rigidbody>();
                Vector3 hitVector = (hit.transform.position - transform.position).normalized;
                rigid.AddForce(hitVector * 1200);
            }
            
            if (alive)
            {
                Die();
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (!alive) return;

        if (other.gameObject.tag == "Bleach")
        {
            audioManager.Play("bleachCollected", false);
            Animator spriteAnimator = other.gameObject.GetComponent<Animator>();
            if (spriteAnimator != null)
            {
                spriteAnimator.SetTrigger("Collected");
            }
            level.bleach++;
        }

        if (other.gameObject.tag == "GatesStreet")
        {
            direction.z = 0;
            level.toogleUI(false, false);
            animator.SetTrigger("Gates");
            audioManager.Play("gatesAlley", false);
        }

        if (other.gameObject.tag == "Laser")
        {
            LaserHit();
        }
    }

    private void LaserHit() {
        Animator spriteAnimator = sprite.GetComponent<Animator>();
        if (spriteAnimator != null)
        {
            spriteAnimator.SetTrigger("LaserHit");
            
            level.bleach -= 5;
            if (level.bleach < 0)
            {
                level.bleach = 0;
                Die();
            } else {
                audioManager.Play("laserOuch", false);
                DropBleach();
            }
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");
        Animator spriteAnimator = sprite.GetComponent<Animator>();
        if (spriteAnimator != null)
        {
            spriteAnimator.SetTrigger("Die");
            audioManager.Play("gameOver", true);
        }
        //level.toogleUI(false, true);
        level.distance = (int) transform.position.z / 4;
        direction.z = 0;
        direction.x = 0;
        alive = false;
    }

    public void OnDyingAnimationEnd()
    {
        LevelManager.gameEnded = true;
    }

    public void onGatesAnimationEnd()
    {
        direction.z = forwardSpeed;
        level.toogleUI(true, false);
        level.awakeGates();

    }

    public void DropBleach(){ 
        var bleachInstance = Instantiate(dropBleach, sprite.transform.position, Quaternion.identity) as GameObject;
        // we remove the bleach prefab after the animation is completed
        Destroy(bleachInstance, 1.3f);
    }
}
