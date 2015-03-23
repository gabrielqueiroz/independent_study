using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class DestroyByContact : MonoBehaviour {
	
	private LevelEditor leveleditor;
	private PlayerController playercontroller;
	private PersistentController persistent;

	public GameObject explosion;

	void Start ()
	{
		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
		{
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
		{
			Debug.Log("Cannot find 'Persistent Controller' script");
		}
		
		GameObject editorOnlyObject = GameObject.FindGameObjectWithTag ("EditorOnly");
		if (editorOnlyObject != null){
			leveleditor = editorOnlyObject.GetComponent <LevelEditor>();
		}

		if (leveleditor == null){
			Debug.Log ("Cannot find 'Level Editor' script");
		}

		GameObject playerControllerObject = GameObject.FindGameObjectWithTag ("Player");
		if (playerControllerObject != null)
		{
			playercontroller = playerControllerObject.GetComponent <PlayerController>();
		}

		if (playercontroller == null)
		{
			Debug.Log ("Cannot find 'Player Controller' script");
		}

	}

	void OnTriggerStay(Collider other) {

		Transform getChild = gameObject.transform.FindChild("Sprite");
		GameObject child = getChild.gameObject;

			if (child.GetComponent<SpriteRenderer> ().isVisible) {
				
				if (other.name.Equals("PointingAt")){				

					Debug.Log ("Pointing at "+child.GetComponent<SpriteRenderer>().sprite.name);
					File.AppendAllText (persistent.getFileName(), "\n Pointing at "+child.GetComponent<SpriteRenderer>().sprite.name);

				} else {
					
					if (gameObject.name.Equals ("ItemWord(Clone)") || gameObject.name.Equals ("ItemPicture(Clone)")){
						leveleditor.AddScore ();
						Instantiate(explosion, transform.position, transform.rotation);
					} else {
						leveleditor.DecScore ();
						playercontroller.damaged = true;
					}
			
					Debug.Log ("Collided with "+child.GetComponent<SpriteRenderer>().sprite.name);
					Destroy (gameObject);
					File.AppendAllText (persistent.getFileName(), "\n Collided with "+child.GetComponent<SpriteRenderer>().sprite.name);
						
				}
			}

	}

	void OnTriggerEnter(Collider other){

	}

	void OnTriggerExit(Collider other){
	
		Transform getChild = gameObject.transform.FindChild("Sprite");
		GameObject child = getChild.gameObject;

		if (child.GetComponent<SpriteRenderer> ().isVisible) {
			Debug.Log("Avoided "+child.GetComponent<SpriteRenderer>().sprite.name);
			File.AppendAllText (persistent.getFileName(), "\n Avoided "+child.GetComponent<SpriteRenderer>().sprite.name);
		}
			
	}
}

