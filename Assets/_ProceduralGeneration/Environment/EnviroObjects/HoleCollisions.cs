using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.ProGen {
    public class HoleCollisions : MonoBehaviour {

        private void OnCollisionEnter2D (Collision2D collision) {
            if (collision.gameObject.tag == "FriendlyBullets") {
                Physics2D.IgnoreCollision (collision.collider, GetComponent<Collider2D> ());
            }
        }

    }
}