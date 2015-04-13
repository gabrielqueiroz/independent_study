using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class DestroyByContact : MonoBehaviour {

	public AudioClip collect;
	private LevelEditor leveleditor;
	private PlayerController playercontroller;
	private PersistentController persistent;
	private string target = "";
	
	public GameObject explosion;


	void Start ()
	{
		collect = Resources.Load <AudioClip>("Sounds/Score");

		GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
		if (persistentObject != null)
		{
			persistent = persistentObject.GetComponent<PersistentController>();
		}
		if (persistent == null)
		{
			Debug.Log("Cannot find 'Persistent Controller' script");
		}
		
		GameObject editorOnlyObject = GameObject.FindGameObjectWithTag ("LevelEditor");
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
					//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" consider "+target);
					persistent.AddLevelLog("\r\n"+persistent.getTime()+" consider "+target);
				}
				
			} else {
				
				if (gameObject.name.Equals ("ItemWord(Clone)") || gameObject.name.Equals ("ItemPicture(Clone)")){
					AudioSource.PlayClipAtPoint(collect, transform.position);
					leveleditor.AddScore ();
					Instantiate(explosion, transform.position, transform.rotation);
				} else {
					leveleditor.DecScore ();
					playercontroller.damaged = true;
				}
				Debug.Log (persistent.getTime()+" collect "+target);
				//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" collect "+target);
				persistent.AddLevelLog("\r\n"+persistent.getTime()+" collect "+target);

				Destroy (gameObject);
				StartCoroutine(waitDestroy());
			}
		}
		
	}

	IEnumerator waitDestroy()
	{
		while (true) {
			yield return new WaitForSeconds(0.5f);
			target = "";
		}
	}

	void OnTriggerExit(Collider other){
		
		Transform getChild = gameObject.transform.FindChild("Sprite");
		GameObject child = getChild.gameObject;
		
		if (child.GetComponent<SpriteRenderer>().isVisible) {
			Debug.Log(persistent.getTime()+" avoid "+child.GetComponent<SpriteRenderer>().sprite.name);
			//File.AppendAllText (persistent.getFileName(), "\r\n"+persistent.getTime()+" avoid "+child.GetComponent<SpriteRenderer>().sprite.name);
			persistent.AddLevelLog( "\r\n"+persistent.getTime()+" collect "+target);
			target = "";
		}		
	}
}