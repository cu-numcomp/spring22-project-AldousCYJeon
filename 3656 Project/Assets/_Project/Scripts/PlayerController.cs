using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Vector2 _input;
    [SerializeField] private soPlayerStatus _status;
    [SerializeField] private Vector2 _calcVel;
    [SerializeField] private bool _wantToMove;
    [SerializeField] private float _friction;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _jumpForce;
    [SerializeField] private InputActionAsset actions;
    [SerializeField] private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        this._rb = GetComponent<Rigidbody2D>();
        this._calcVel = new Vector2();

        actions["Jump"].performed += ctx => ApplyJump();

        actions["Move"].performed += ctx => ApplyVelocity(ctx.ReadValue<UnityEngine.Vector2>());
        actions["Move"].canceled += ctx => ApplyVelocity(ctx.ReadValue<UnityEngine.Vector2>());

        actions.Enable();

        this._wantToMove = false;
        this._status.IsGrounded = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!this._status.IsGrounded)
        {
            this._calcVel.y += -1f;
        }
        if (!this._wantToMove)
        {
            if (this._status.IsGrounded)
            {
                var fric = -1f * this._friction;
                if ((Mathf.Abs(this._calcVel.x) + fric) <= 0)
                {
                    this._calcVel.x = 0f;
                }
                else
                {
                    var axisOnly = new Vector2(this._calcVel.normalized.x, 0f).normalized.x;
                    this._calcVel.x += fric * axisOnly;
                }
            }
        }
        else if (this._status.IsGrounded)
        {
            var axisOnly = new Vector2(this._input.x, 0f).normalized.x;
            this._calcVel.x += this._acceleration * axisOnly;

            if (Mathf.Abs(this._calcVel.x) > this._runSpeed)
            {
                this._calcVel.x = axisOnly * this._runSpeed;
            }

        }
        this._rb.velocity = this._calcVel;
    }

    private void ApplyJump()
    {
        if (this._status.IsGrounded)
        {
            this._calcVel.y = this._jumpForce;
        }
    }

    private void ApplyVelocity(Vector2 move)
    {

        var hold = move;
        hold.y = 0f;
        float facing = hold.normalized.x;

        if (facing == 0)
        {
            facing = this._animator.transform.localScale.x;
        }


        this._animator.transform.localScale = new Vector3(facing, this._animator.transform.localScale.y, this._animator.transform.localScale.z);

        this._input = move;
        this._wantToMove = move.x != 0f;

        this._animator.SetBool("move", this._wantToMove);
    }
}
