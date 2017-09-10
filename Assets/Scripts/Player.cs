using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {

    // control animation in code, so get the animator reference
    [SerializeField]
    private Animator animator;
    
    // control player gravity
    private Rigidbody rigid;

    // the compoennt that play the sound player make
    private AudioSource audioSrc;

    // player jump sound
    [SerializeField]
    private AudioClip jumpSfx;

    [SerializeField]
    private AudioClip deathSfx;

    private bool firstJumpInitialized = false;
    
    // field assertion
    private void Awake()
    {
        Assert.IsNotNull(animator);
        Assert.IsNotNull(jumpSfx);
        Assert.IsNotNull(deathSfx);
    }

    // Use this for initialization
    void Start () {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;

        audioSrc = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.PlayerActivated) {
            // left mouse button clicked or finger tapped
            // jump
            animator.Play("Jump");
        }
	}

    // for physic stuff
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.GameStarted) {
            // perform the jump action
            Jump();
        }
    }
    
    // initialization after first jump
    void DidFirstJump() {
        // open gravity
        rigid.useGravity = true;

        // tell game manager that player started the first jump
        GameManager.instance.OnPlayerActivated();
    }

    void Jump() {

        if (!firstJumpInitialized) {
            DidFirstJump();
            firstJumpInitialized = true;
        }

        if (GameManager.instance.PlayerActivated) {
            // reset velocity of the rigidbody
            rigid.velocity = Vector3.zero;
            
            // do the real jump
            rigid.AddForce(new Vector3(0f, 100f, 0f), ForceMode.Impulse);
    
            // player the jump sound
            audioSrc.PlayOneShot(jumpSfx);
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {

        // play the death sound
        audioSrc.PlayOneShot(deathSfx);

        // throw the player back and cancel collision detection
        rigid.AddForce(new Vector3(-50f, 0f, 0f), ForceMode.Impulse);

        rigid.detectCollisions = false;

        // tell the GameManager that the player has hit a obstacle and the game is now over
        GameManager.instance.OnGameOver();

        // Destory the player
        Destroy(gameObject);
    
    }
}
