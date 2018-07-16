using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    private Animator _anim;

	// Use this for initialization
	void Start () {
        _anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AirBorn(bool inAir)
    {
        _anim.SetBool("InAir", inAir);
    }


    public void Jump(bool jump)
    {
        _anim.SetBool("Jumping", jump);
    }

    public void Move(float move)
    {
        _anim.SetFloat("Move",Mathf.Abs(move));
    }
}
