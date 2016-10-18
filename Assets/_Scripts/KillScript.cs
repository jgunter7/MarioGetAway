/*
 *      File:                   KillScript.cs
 *      Authors Name:           Jason Gunter
 *      Last Modified By:       Jason Gunter
 *      Date Last Modified:     October 18th, 2016
 *      Description:            A unity platform game featuring mario escaping from jail :) - jgunter
 *      Revision History:       https://github.com/jgunter7/MarioGetAway/commits/master 
 */
using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

    // This script can be attached to other items that fall
    //  ANYTHING ATTACHED TO THIS SCRIPT WILL BE DESTROYED WHEN IT HITS THE DEATH PLANE.
	private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            Destroy(this.gameObject);
        }
    }
}
