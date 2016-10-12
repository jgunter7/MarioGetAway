using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //Private Variables:
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private float _move;
    private float _jump;
    private bool _isFacingRight;
    private bool _isGrounded;

    //Public Variables:
    public float Velocity = 20f;
    public float JumpForce = 100f;
    public Camera cam;
    public Transform SpawnPoint;
    public Transform DeathPlane;

    [Header("Sound Clips")]
    public AudioSource JumpSound;
    public AudioSource DeathSound;

	// Use this for initialization
	void Start () {
        this._initialize();
	}
	
	// FIXEDUPDATE is called once per frame (Physics)
	void FixedUpdate () {
        if (this._isGrounded) {
            this._move = Input.GetAxis("Horizontal");
            if (this._move > 0f) {
                this._move = 1f;
                this._isFacingRight = true;
                this._flip();
            } else if (this._move < 0f) {
                this._move = -1f;
                this._isFacingRight = false;
                this._flip();
            } else {
                this._move = 0f;
            }
        
            if (Input.GetKeyDown(KeyCode.Space)) {
                this._jump = 1f;
                this.JumpSound.Play();
            }

            this._rigidbody.AddForce(new Vector2(
                this._move * this.Velocity,
                this._jump * this.JumpForce), ForceMode2D.Force);
        } else {
            this._jump = 0f;
        }

        this.cam.transform.position = new Vector3(this._transform.position.x, this._transform.position.y, -10f);
        this.DeathPlane.position = new Vector3(this._transform.position.x, -12, 0f);
	}

    // Private Methods:
    /*
     * This method initializes anything I need - jgunter
     */
     private void _initialize() {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._move = 0;
        this._isFacingRight = true;
        this._isGrounded = false;
    }

    /*
     * This method flips mario accross the X axis
     */
     private void _flip() {
        if (this._isFacingRight) {
            this._transform.localScale = new Vector2(1f, 1f);
        } else {
            this._transform.localScale = new Vector2(-1f, 1f);
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            this._isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        this._isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            //sounds and respawn here. - jgunter
            this.DeathSound.Play();
            this._transform.position = this.SpawnPoint.position;
        }
    }
}
