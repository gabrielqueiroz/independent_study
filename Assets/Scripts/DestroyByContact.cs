using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	private LevelEditor leveleditor;
	private PlayerController playercontroller;
    public GameObject explosion;

	void Start ()
	{
		GameObject editorOnlyObject = GameObject.FindGameObjectWithTag ("EditorOnly");
		if (editorOnlyObject != null)
		{
			leveleditor = editorOnlyObject.GetComponent <LevelEditor>();
		}
		if (leveleditor == null)
		{
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

	void Update(){
	
	}

	void OnTriggerEnter (Collider other)
	{

		Debug.Log (gameObject.name);
		if (gameObject.name.Equals ("ItemWord(Clone)") || gameObject.name.Equals ("ItemPicture(Clone)")){
			leveleditor.AddScore ();
            Instantiate(explosion, transform.position, transform.rotation);
		} else {
			leveleditor.DecScore ();
			playercontroller.damaged = true;
		}
		Destroy (gameObject);
	}



}

