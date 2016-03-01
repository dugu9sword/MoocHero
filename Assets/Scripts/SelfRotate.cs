using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {

    //public GameObject rotateCenter;
	public float rotateSpeed = 90.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //if (rotateCenter == null)
        //    rotateCenter = gameObject;
        //Debug.Log ("==="+rotateCenter.transform.up);
		GetComponent<Transform>().Rotate(Vector3.up,Time.deltaTime*rotateSpeed,0);
	}
}
