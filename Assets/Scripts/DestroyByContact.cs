using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;

public class DestroyByContact : MonoBehaviour {

    public AudioClip score;
    public AudioClip wrong;
    private LevelEditor leveleditor;
    private PlayerController playercontroller;
    private PersistentController persistent;
    private string target = "";
    public GameObject explosion;

    void Start(){
        score = Resources.Load <AudioClip>("Sounds/Score");
        wrong = Resources.Load <AudioClip>("Sounds/wrong_sound");

        GameObject persistentObject = GameObject.FindGameObjectWithTag("Persistent");
        if(persistentObject != null){
            persistent = persistentObject.GetComponent<PersistentController>();
        }
        if(persistent == null){
            Debug.Log("Cannot find 'Persistent Controller' script");
        }
        
        GameObject editorOnlyObject = GameObject.FindGameObjectWithTag("LevelEditor");
        if(editorOnlyObject != null){
            leveleditor = editorOnlyObject.GetComponent <LevelEditor>();
        }
        
        if(leveleditor == null){
            Debug.Log("Cannot find 'Level Editor' script");
        }
        
        GameObject playerControllerObject = GameObject.FindGameObjectWithTag("Player");
        if(playerControllerObject != null){
            playercontroller = playerControllerObject.GetComponent <PlayerController>();
        }
        
        if(playercontroller == null){
            Debug.Log("Cannot find 'Player Controller' script");
        }
        
    }

    void Update(){

    }

    /**
     * Using on trigger stay to receive an upate per frame until the object is rendered in the screen
     */
    
    void OnTriggerStay(Collider other){
        
        Transform getChild = gameObject.transform.FindChild("Sprite");
        GameObject child = getChild.gameObject;

        if(other.name.Equals("PointingAt") || other.name.Equals("Player")){
            if(other.name.Equals("PointingAt")){    
                if(!target.Equals(child.GetComponent<SpriteRenderer>().sprite.name)){
                    target = child.GetComponent<SpriteRenderer>().sprite.name;
                    Debug.Log(persistent.getTime() + " consider " + target);
                    persistent.AddLevelLog("\r\n" + persistent.getTime() + " consider " + target);
                }
                    
            } else{
                Vector3 targetDir = transform.position - other.transform.position;
                float angle = Vector3.Angle(other.transform.forward, targetDir);
                target = child.GetComponent<SpriteRenderer>().sprite.name;
                if(gameObject.name.Equals("ItemText(Clone)") || gameObject.name.Equals("ItemPicture(Clone)")){
                    AudioSource.PlayClipAtPoint(score, transform.position);
                    Debug.Log(persistent.getTime() + " collect good " + target + " angle " + angle);
                    persistent.AddLevelLog("\r\n" + persistent.getTime() + " collect good " + target + " angle " + angle);
                    leveleditor.AddScore();
                    Instantiate(explosion, transform.position, transform.rotation);
                    StartCoroutine(waitDestroy());
                } else{
                    AudioSource.PlayClipAtPoint(wrong, transform.position);
                    Debug.Log(persistent.getTime() + " collect bad " + target + " angle " + angle);
                    persistent.AddLevelLog("\r\n" + persistent.getTime() + " collect bad " + target + " angle " + angle);
                    leveleditor.DecScore();
                    playercontroller.damaged = true;
                    StartCoroutine(waitDestroy());
                }
                Destroy(gameObject);
            }
                
        }

    }

    IEnumerator waitDestroy(){
        while(true){
            yield return new WaitForSeconds(0.5f);
            target = "";
        }
    }

    void OnTriggerExit(Collider other){
        
        Transform getChild = gameObject.transform.FindChild("Sprite");
        GameObject child = getChild.gameObject;

        if(!target.Equals("")){
            Debug.Log(persistent.getTime() + " avoid " + child.GetComponent<SpriteRenderer>().sprite.name);
            persistent.AddLevelLog("\r\n" + persistent.getTime() + " avoid " + target);
            target = "";
        }
    
    }
}