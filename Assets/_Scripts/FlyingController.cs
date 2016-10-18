/*
 *      File:                   FlyingController.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
using UnityEngine;
using System.Collections;

public class FlyingController : MonoBehaviour {

    //PRIVATE VARIABLES
    private Transform _transform;
    private float _startingPoint;
    private float _speed = 0.025f;
    private float _range = 2.5f;
    private bool _goingUp = true;

    // Use this for initialization
    void Start() {
        this._transform = GetComponent<Transform>();
        this._startingPoint = this._transform.position.y;
    }

    // Update is called once per frame
    //  this function moves any flying characters around the scene.
    void Update() {
        if (this._goingUp) { // GOING UPWARDS!
            if (this._transform.position.y >= this._startingPoint + this._range) {
                this._goingUp = false;
            } else {
                this._transform.position = new Vector3(this._transform.position.x, this._transform.position.y + this._speed, this._transform.position.z);
            }
        } else { // GOING DOWNWARDS!
            if (this._transform.position.y <= this._startingPoint - this._range) {
                this._goingUp = true;
            } else {
                this._transform.position = new Vector3(this._transform.position.x, this._transform.position.y - this._speed, this._transform.position.z);
            }
        }
    }
}
