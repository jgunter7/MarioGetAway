/*
 *      File:                   CheckPointController.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
using UnityEngine;
using System.Collections;

public class CheckPointController : MonoBehaviour {
    // PRIVATE VARIABLES
    private Transform _transform;
    private GameObject spawnPoint;

	// Use this for initialization
	void Start () {
        this._transform = GetComponent<Transform>();
        this.spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Once the checkpoint is passed, move the spawn point up to the checkpoints position. - jgunter
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            spawnPoint.transform.position = this._transform.position;
        }
    }
}
