using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedTrigger : MonoBehaviour
{
    [SerializeField] private soPlayerStatus _status;

    void OnTriggerEnter2D(Collider2D other)
    {
        this._status.IsGrounded = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        this._status.IsGrounded = false;
    }
}
