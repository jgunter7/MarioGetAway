/*
 *      File:                   PlayerController.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
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
    private Animator _animator;
    private GameObject _cam;
    private GameObject _SpawnPoint;
    private GameObject _DeathPlane;
    private GameObject _gameControllerObject;
    private GameController _gameController;

    //Public Variables:
    [Header("Player Movement")]
    public float Velocity = 10f;
    public float JumpForce = 100f;

    [Header("Sound Clips")]
    public AudioSource JumpSound;
    public AudioSource DeathSound;
    public AudioSource CoinSound;
    public AudioSource EnemyDeathSound;

    [Header("Ray-Casting Objects")]
    public Transform SightStart;
    public Transform SightEnd;
    public Transform SightEndTwo;

    // Use this for initialization
    void Start() {
        this._initialize();
    }

    // FIXEDUPDATE is called once per frame (Physics)
    //  move the player around in the scene
    void FixedUpdate() {
        if (this._isGrounded) {
            this._move = Input.GetAxis("Horizontal");
            if (this._move > 0f) {
                this._move = 1f;
                this._isFacingRight = true;
                this._animator.SetInteger("HeroState", 1);
                this._flip();
            } else if (this._move < 0f) {
                this._move = -1f;
                this._animator.SetInteger("HeroState", 1);
                this._isFacingRight = false;
                this._flip();
            } else {
                this._move = 0f;
                this._animator.SetInteger("HeroState", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                this._jump = 1f;
                this._animator.SetInteger("HeroState", 2);
                this.JumpSound.Play();
            }

            this._rigidbody.AddForce(new Vector2(
                this._move * this.Velocity,
                this._jump * this.JumpForce), ForceMode2D.Force);
        } else {
            this._jump = 0f;
        }

        this._cam.transform.position = new Vector3(this._transform.position.x, this._transform.position.y, -10f);
        this._DeathPlane.transform.position = new Vector3(this._transform.position.x, -12, 0f);

        this._isGrounded = (Physics2D.Linecast(
                this.SightStart.position, this.SightEnd.position,
                1 << LayerMask.NameToLayer("Solid"))
                ||
                Physics2D.Linecast(
                this.SightStart.position, this.SightEndTwo.position,
                1 << LayerMask.NameToLayer("Solid")));
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
        this._animator = GetComponent<Animator>();
        this._cam = GameObject.FindWithTag("MainCamera");
        this._SpawnPoint = GameObject.FindWithTag("SpawnPoint");
        this._DeathPlane = GameObject.FindWithTag("DeathPlane");
        this._gameControllerObject = GameObject.Find("GameController");
        this._gameController = this._gameControllerObject.GetComponent<GameController>() as GameController;
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

    private void OnCollisionExit2D(Collision2D other) {
        this._isGrounded = false;
        this._animator.SetInteger("HeroState", 2);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Winner") && this._isGrounded) {
            this._gameController.LivesValue = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            //sounds and respawn here. - jgunter
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            this._transform.position = this._SpawnPoint.transform.position;
        } else if (other.gameObject.CompareTag("Enemy_Mush")) {
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            this._transform.position = this._SpawnPoint.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        // Any triggers are handled here:
        if (other.gameObject.CompareTag("Enemy_Mush")) {
            this.EnemyDeathSound.Play();
            this._gameController.ScoreValue += 50;
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Coin")) {
            this.CoinSound.Play();
            Destroy(other.gameObject);
            this._gameController.ScoreValue += 10;
        } else if (other.gameObject.CompareTag("Enemy_Helo")) {
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            this._transform.position = this._SpawnPoint.transform.position;
        }
    }
}
