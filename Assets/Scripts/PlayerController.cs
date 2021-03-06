﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

    //Public Vars
    public float rotationSpeed;
    public GameObject spaceship;
    public Sprite normal, propulsion;
    private float speed = 7f;

    //Private Vars
    private Vector3 mousePosition;
    private GameObject Canvas;
    private Image damageImage;
    private PersistentController persistent;
    public float flashSpeed = 0.1f;
    public Color flashColour = new Color(1f, 0f, 0f, 1f);
    public bool damaged = false;
    private bool accelerate = false;
    private float time = 0f;
    private bool sound = true;

    void Start(){
        Canvas = GameObject.Find("Canvas");
        Transform getChild = Canvas.transform.FindChild("Damage");
        GameObject child = getChild.gameObject;
        damageImage = child.GetComponent<UnityEngine.UI.Image>();
        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
    }

    void Update(){
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        // then find the point along that ray that meets that distance.  This will be the point
        // to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if(playerPlane.Raycast(ray, out hitdist) && Input.GetMouseButton(0)){
            //Log when player "accelerates"
            if(!accelerate){
                time = Time.time;
                Debug.Log(persistent.getTime() + " accelerate ");
                persistent.AddLevelLog("\r\n" + persistent.getTime() + " accelerate ");
                accelerate = true;
            }
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);
            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            // Add force in order to accelerate or slow down the spaceship
            if(GetComponent<Rigidbody>().velocity.magnitude <= 5.0f)
                GetComponent<Rigidbody>().AddForce(transform.forward * speed);    
            // Change the sprite that have the propulsion
            spaceship.GetComponent<SpriteRenderer>().sprite = propulsion;
            // Add sound
            if(sound){
                spaceship.GetComponent<AudioSource>().Play();
                sound = false;
            }
        } else{
            // Change the sprite that don't have the propulsion
            spaceship.GetComponent<SpriteRenderer>().sprite = normal;
            if(accelerate){
                float delta = Time.time - time;
                persistent.AddLevelLog("\r\n" + persistent.getTime() + " brake " + delta.ToString());
                Debug.Log(persistent.getTime() + " brake " + delta.ToString());
                time = 0f;
                accelerate = false;
            }
            sound = true;
            spaceship.GetComponent<AudioSource>().Stop();
        }

        if(damaged){
            damageImage.color = flashColour;
        } else{
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        
        damaged = false;
    }

    IEnumerator damagePerSecond(){
        for(int i = 0; i <= 2; i++){
            yield return new WaitForSeconds(0.1f);
            damageImage.color = flashColour;
        }
    }
}