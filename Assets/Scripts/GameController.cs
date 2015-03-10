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
		details.Add (2, "Find and collect three frogs");
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
