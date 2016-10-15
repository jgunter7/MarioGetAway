using UnityEngine;
using System.Collections;

public class KillScript : MonoBehaviour {

    // This script can be attached to other items that fall
	private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("DeathPlane")) {
            Destroy(this.gameObject);
        }
    }
}
