using UnityEngine;
using System.Collections;

public class BallShoot : MonoBehaviour {

	public int force;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody> ().AddForce (new Vector3(0,-force,0));
	}
}
