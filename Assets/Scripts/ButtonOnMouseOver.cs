using UnityEngine;
using System.Collections;

public class ButtonOnMouseOver : MonoBehaviour {

    bool resize = true;

    void OnMouseOver(){
        if(resize){
            transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
            resize = false;
        }
    }

    void OnMouseExit(){
        resize = true;
        transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
    }
}
