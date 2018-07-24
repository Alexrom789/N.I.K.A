using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerksMovement : MonoBehaviour {

// Use this for initialization
	void Awake () {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 100f)){
            //Debug.Log(hit.point);
            //nav.updatePosition = true;
            transform.position = hit.point;
        }
	}

}
