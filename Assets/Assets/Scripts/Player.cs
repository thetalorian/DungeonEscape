using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    Rigidbody2D _rigid;
    [SerializeField]
    float _jumpForce = 5f;
    [SerializeField]
    bool _grounded = false;
    [SerializeField]
    LayerMask _groundLayer;
    [SerializeField]
    float _moveSpeed = 2f;
    bool _jumpCooldown = false;

	// Use this for initialization
	void Start () {
        _rigid = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}


    void Movement() {
        float move = Input.GetAxisRaw("Horizontal") * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(jumpCooldownRoutine());

        }
        _rigid.velocity = new Vector2(move, _rigid.velocity.y);
    }
    bool IsGrounded() {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.down, .7f, _groundLayer.value);
        //Debug.DrawRay(transform.position, Vector2.down * 0.7f, Color.red);
        if (hitInfo.collider != null)
        {
            if (!_jumpCooldown) return true;
        }
        return false;

    }

    IEnumerator jumpCooldownRoutine () {
        _jumpCooldown = true;
        yield return new WaitForSeconds(0.1f);
        _jumpCooldown = false;
    }
}
