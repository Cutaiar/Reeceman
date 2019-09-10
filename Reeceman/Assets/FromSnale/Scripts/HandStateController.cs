using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStateController : MonoBehaviour {

    public Animator anim;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Excite()
    {
        //anim.SetInteger("HandState", 1);
    }

    public void UnExcite()
    {
        anim.SetInteger("HandState", 0);
    }
}
