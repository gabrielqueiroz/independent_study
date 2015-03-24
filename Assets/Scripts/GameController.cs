using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

	public Dictionary<int, string> details = new Dictionary<int, string> ();
	public Dictionary<int, int> score = new Dictionary<int, int> ();

	public int actualLevel;

	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start(){			
		details.Add (1, "Find and collect three cats");
		details.Add (2, "Find and collect three cars");
        details.Add(3, "Find and collect three pencils");
        details.Add(4, "Find and collect three frogs");
        details.Add(5, "Find the word that describes this object three times");
        details.Add(6, "Find the word that describes this object three times");
        details.Add(7, "Find the word that describes this object three times");
        details.Add(8, "Find three different words that describe this object");
        details.Add(9, "Find three different words that describe this object");
        details.Add(10, "Find three different words that describe this object");
        details.Add(11, "Find three different words that DO NOT describe this object");
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel(0);
	}

	public void setLevelScore(int score) {
		this.score[actualLevel] = score;
	}
	
	public void setActualLevel(int level){
		this.actualLevel = level;
		Application.LoadLevel(1);
	}

}
