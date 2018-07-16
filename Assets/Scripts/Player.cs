using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D _rigid;
    [SerializeField]
    float _jumpForce = 5f;
    [SerializeField]
    LayerMask _groundLayer;
    [SerializeField]
    float _moveSpeed = 2f;

    [SerializeField]
    bool _jumping = false;
    [SerializeField]
    bool _grounded = true;


    private PlayerAnimation _anim;
    private SpriteRenderer spriteRender;

	// Use this for initialization
	void Start () {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        spriteRender = GetComponentInChildren<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        _grounded = IsGrounded();
        Movement();
	}


    void Movement() {
        float move = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        if (move < 0)
        {
            spriteRender.flipX = true;
        }
        if (move > 0) {
            spriteRender.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _grounded && CanJump())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(jumpRoutine());
        }
        _rigid.velocity = new Vector2(move, _rigid.velocity.y);
        _anim.Move(move);
    }

    bool CanJump()
    {
        if (_jumping)
        {
            return false;
        }
        return true;
    }

    bool IsGrounded() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .8f, _groundLayer.value);
        //Debug.DrawRay(transform.position, Vector2.down * 0.8f, Color.red, 5f, true);
        //Debug.Log("Checking Hit at " + transform.position.ToString());
        if (hitInfo.collider != null)
        {
          _anim.AirBorn(false);
          return true;
        }
        _anim.AirBorn(true);
        return false;

    }

    IEnumerator jumpRoutine () {
        _jumping = true;
        _anim.Jump(true);
        yield return new WaitForSeconds(1f);
        _jumping = false;
        _anim.Jump(false);
    }
}
