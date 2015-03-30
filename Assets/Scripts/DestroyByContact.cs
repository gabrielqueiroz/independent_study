using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class DestroyByContact : MonoBehaviour {
	
	private LevelEditor leveleditor;
	private PlayerController playercontroller;
	private PersistentController persistent;
	private string target = "";
	
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
	
	/**
	 * Using on trigger stay to receive an upate per frame until the object is rendered in the screen
	 */
	
	void OnTriggerStay(Collider other) {
		
		Transform getChild = gameObject.transform.FindChild("Sprite");
		GameObject child = getChild.gameObject;
		
		if (child.GetComponent<SpriteRenderer>().isVisible) {
			
			if (other.name.Equals("PointingAt")){	
				
				if (!target.Equals(child.GetComponent<SpriteRenderer>().sprite.name)){
					target = child.GetComponent<SpriteRenderer> ().sprite.name;
					Debug.Log (persistent.getTime()+" consider "+target);
					File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" consider "+target);
				}
				
			} else {
				
				if (gameObject.name.Equals ("ItemWord(Clone)") || gameObject.name.Equals ("ItemPicture(Clone)")){
					leveleditor.AddScore ();
					Instantiate(explosion, transform.position, transform.rotation);
				} else {
					leveleditor.DecScore ();
					playercontroller.damaged = true;
				}

				Debug.Log (persistent.getTime()+" collect "+target);
				File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" collect "+target);
				target = "";
				Destroy (gameObject);

			}
		}
		
	}
	
	void OnTriggerExit(Collider other){
		
		Transform getChild = gameObject.transform.FindChild("Sprite");
		GameObject child = getChild.gameObject;
		
		if (child.GetComponent<SpriteRenderer>().isVisible) {
			Debug.Log(persistent.getTime()+" avoid "+child.GetComponent<SpriteRenderer>().sprite.name);
			File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" avoid "+child.GetComponent<SpriteRenderer>().sprite.name);
			target = "";
		}
		
	}
}