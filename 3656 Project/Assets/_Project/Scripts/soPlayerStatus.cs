using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObjects/PlayerStatus", order = 1)]

public class soPlayerStatus : ScriptableObject
{
    [SerializeField] private bool _isGrounded;

    public bool IsGrounded
    {
        get { return this._isGrounded; }
        set { this._isGrounded = value; }
    }
}
