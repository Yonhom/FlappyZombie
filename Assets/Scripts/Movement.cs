using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    [SerializeField]
    private float speed = 1.0f;

    private float reappearX = 138f;
    private float disappearX = -20.0f;
	
	// Update is called once per frame
	protected void Update () {

        if (!GameManager.instance.GameStarted) {
            return;
        }

        if (GameManager.instance.PlayerActivated && gameObject.tag == "Obstacle") {
            TranslateLeft();
        }
        
        if(!GameManager.instance.GameOver && gameObject.tag == "Platform") {
            TranslateLeft();
        }
		
	}
    
    void TranslateLeft() {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // reset the platform's position if any of the platform is out of screen view
        if (transform.position.x <= disappearX) {
            transform.position = new Vector3(reappearX, transform.position.y, transform.position.z);
        }
    }
}
