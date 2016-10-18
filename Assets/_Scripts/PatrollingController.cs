/*
 *      File:                   PatrollingController.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
using UnityEngine;
using System.Collections;

/*
 *      THIS CONTROLLER IS FOR ANY PATROLLING ENEMIES THAT MAY BE USED WITHIN THE GAME.
 *      THE ENEMIES WALK FROM ONE SIDE OF THE PLATFORM TO THE OTHER, AND BECOME AGGRVATED
 *      WHEN THE PLAYER WALKS INTO THEIR FOV (FIELD OF VIEW/VISION)
 */
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

            // USE LINECASTING TO DETERMINE IF WE CAN CONTINUE TO WALK.
            this._isGroundAhead = Physics2D.Linecast(
                this.SightStart.position, this.SightEnd.position,
                1 << LayerMask.NameToLayer("Solid"));

            // USE LINECASTING TO DETERMINE IF WE CAN SEE THE PLAYER'S CHARACTER AHEAD.
            this._isPlayerDetected = Physics2D.Linecast(
                this.SightStart.position,
                this.SightLine.position,
                1 << LayerMask.NameToLayer("Player"));

            // check for ground ahead fo the enemy to walk, if no ground, walk the other way. - jgunter
            if (!this._isGroundAhead) {
                this._flip();
            }

            // INCREASE SPEED IF THE PLAYER IS DETECTED (AGGREVATED)
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

    // FLIP THE PLAYERS SCALE ON THE X AXIS TO A NEGATIVE/POSITIVE VALUE
    private void _flip() {
        if (this._transform.localScale.x == 1) {
            this._transform.localScale = new Vector2(-1f, 1f);
        } else {
            this._transform.localScale = new Vector2(1f, 1f);
        }
    }
}
