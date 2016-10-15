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
    public float Velocity = 20f;
    public float JumpForce = 100f;

    [Header("Sound Clips")]
    public AudioSource JumpSound;
    public AudioSource DeathSound;
    public AudioSource CoinSound;

    // Use this for initialization
    void Start() {
        this._initialize();
    }

    // FIXEDUPDATE is called once per frame (Physics)
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

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.CompareTag("Platform")) {
            this._isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        this._isGrounded = false;
        this._animator.SetInteger("HeroState", 2);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            //sounds and respawn here. - jgunter
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            this._transform.position = this._SpawnPoint.transform.position;
        } else if(other.gameObject.CompareTag("Coin")) {
            this.CoinSound.Play();
            Destroy(other.gameObject);
            this._gameController.ScoreValue += 10;
        } else if (other.gameObject.CompareTag("Enemy_Mush")) {
            this.DeathSound.Play();
            this._gameController.LivesValue -= 1;
            this._transform.position = this._SpawnPoint.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Enemy_Mush")) {
            GetComponent<AudioSource>().Play(); //get the only audio source and play it. - jgunter
            this._gameController.ScoreValue += 50;
            Destroy(other.gameObject);
        }
    }
}
