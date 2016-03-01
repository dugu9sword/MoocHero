using UnityEngine;
using System.Collections;

public class RotateAroundAndLookAt : MonoBehaviour {

	public GameObject rotateCenter;
	private Vector3 bias=new Vector3(100.0f,0.0f,100.0f);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (rotateCenter) {
			//Debug.Log(rotateCenter.transform.up+" "+rotateCenter.transform.position);
			transform.RotateAround (rotateCenter.transform.position-bias,rotateCenter.transform.up, Time.deltaTime * 10.0f);
			transform.LookAt(rotateCenter.transform);
		}
	} 
}
