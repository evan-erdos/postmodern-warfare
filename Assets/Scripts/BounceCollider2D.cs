using UnityEngine;
using System.Collections;

public class BounceCollider2D : MonoBehaviour {

    public float force = 20f;

    void OnTriggerStay2D(Collider2D c) {
        if (!c.attachedRigidbody) return;
        var normal = new Vector2(
        	transform.up.x,
        	transform.up.y);
        c.attachedRigidbody.AddForce(normal*force,ForceMode2D.Impulse);
        //foreach(ContactPoint2D hit in c.contacts)
        //    c.attachedRigidbody.AddForce(hit.normal*force,ForceMode2D.Impulse);
    }
}
