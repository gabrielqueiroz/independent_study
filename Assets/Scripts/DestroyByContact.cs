using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	private LevelEditor leveleditor;
	private float duration = 10.0f;
	private float magnitude = 5.0f;
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

	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log (gameObject.name);
		if (gameObject.name.Equals ("ItemWord(Clone)") || gameObject.name.Equals ("ItemPicture(Clone)"))
			leveleditor.AddScore ();
		else {
			Shake();
			leveleditor.DecScore ();
		}
		Destroy (gameObject);
	}

	IEnumerator Shake() {
		
		float elapsed = 0.0f;
		
		Vector3 originalCamPos = Camera.main.transform.position;
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;          
			
			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
			
			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;
			
			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
			
			yield return null;
		}
		
		Camera.main.transform.position = originalCamPos;
	}

	}

