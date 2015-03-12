using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//Public Vars
	public float rotationSpeed;
	public GameObject spaceship;
	public Sprite normal, propulsion;

	//Private Vars
	private Vector3 mousePosition;

	public Image damageImage;
	public float flashSpeed = 0.1f;
	public Color flashColour = new Color (1f, 0f, 0f, 1f);
	
	public bool damaged = false;

	void Update ()
	{
		// Generate a plane that intersects the transform's position with an upwards normal.
		Plane playerPlane = new Plane (Vector3.up, transform.position);
		// Generate a ray from the cursor position
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		// Determine the point where the cursor ray intersects the plane.
		// This will be the point that the object must look towards to be looking at the mouse.
		// Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
		// then find the point along that ray that meets that distance.  This will be the point
		// to look at.
		float hitdist = 0.0f;
		// If the ray is parallel to the plane, Raycast will return false.
		if (playerPlane.Raycast (ray, out hitdist) && Input.GetMouseButton (0)) {

			// Get the point along the ray that hits the calculated distance.
			Vector3 targetPoint = ray.GetPoint (hitdist);
			// Determine the target rotation.  This is the rotation if the transform looks at the target point.
			Quaternion targetRotation = Quaternion.LookRotation (targetPoint - transform.position);
			// Smoothly rotate towards the target point.
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
			// Add force in order to accelerate or slow down the spaceship
			GetComponent<Rigidbody>().AddForce (transform.forward * 10);	
			// Change the sprite that have the propulsion
			spaceship.GetComponent<SpriteRenderer> ().sprite = propulsion;

		} else {
			// Change the sprite that don't have the propulsion
			spaceship.GetComponent<SpriteRenderer> ().sprite = normal;

		}

		if(damaged) {
			damageImage.color = flashColour;
		} else {
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		
		damaged = false;
	}

	IEnumerator damagePerSecond(){
		for (int i = 0; i <= 2; i++) {
			yield return new WaitForSeconds (0.1f);
			damageImage.color = flashColour;
		}
	}

}