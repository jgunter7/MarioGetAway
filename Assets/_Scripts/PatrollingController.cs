using UnityEngine;
using System.Collections;

public class PatrollingController : MonoBehaviour {
    // PRIVATE VARIABLES
    private Transform _transform;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private bool _isGroundAhead;
    private bool _isPlayerDetected;

    //PUBLIC VARIABLES
    public float Speed = 0.5f;
    public Transform SightStart;
    public Transform SightEnd;
    public Transform SightLine;

	// Use this for initialization
	void Start () {
        this._transform = GetComponent<Transform>();
        this._rigidbody = GetComponent<Rigidbody2D>();
        this._isGrounded = false;
        this._isGroundAhead = true;
        this._isPlayerDetected = false;
	}
	
	// Update is called once per frame (Physics - AKA, use FixedUpdate)
	void FixedUpdate () {
        if (this._isGrounded) {
            this._rigidbody.velocity = new Vector2(this._transform.localScale.x, 0) * this.Speed;

            this._isGroundAhead = Physics2D.Linecast(
                this.SightStart.position, this.SightEnd.position,
                1 << LayerMask.NameToLayer("Solid"));

            this._isPlayerDetected = Physics2D.Linecast(
                this.SightStart.position,
                this.SightLine.position,
                1 << LayerMask.NameToLayer("Player"));

            // check for ground ahead fo the enemy to walk, if no ground, walk the other way. - jgunter
            if (!this._isGroundAhead) {
                this._flip();
            }

            if (this._isPlayerDetected) {
                this.Speed *= 1.05f;
                if (this.Speed >= 2f) {
                    this.Speed = 2f;
                }
            }
        }
	}

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            this._isGrounded = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Enemy_Mush")) {
            this._flip();
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            this._isGrounded = false;
        }
    }

    private void _flip() {
        if (this._transform.localScale.x == 1) {
            this._transform.localScale = new Vector2(-1f, 1f);
        } else {
            this._transform.localScale = new Vector2(1f, 1f);
        }
    }
}
