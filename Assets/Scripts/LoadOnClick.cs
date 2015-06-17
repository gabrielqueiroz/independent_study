using UnityEngine;
using System.Collections;

public class LoadOnClick : MonoBehaviour {

    private PersistentController persistent;
    public int level;

    void Start(){     
        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
    }

    void OnMouseDown(){
        if(level == 1)
            Application.LoadLevel(1);
        else if(level == 99999){
            GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
            Destroy(persistentObject);
            Application.LoadLevel(0);
        } else if(!persistent.checkIfComplete(level - 2)){
            persistent.setCurrentLevel(level-2);
            Application.LoadLevel(3);
        } else
            Application.LoadLevel(1);
    }

}
