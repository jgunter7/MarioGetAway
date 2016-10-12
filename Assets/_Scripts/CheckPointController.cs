﻿using UnityEngine;
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

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            spawnPoint.transform.position = this._transform.position;
        }
    }
}
