using UnityEngine;
using System.Collections;

public class spark : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var ps = GetComponent<ParticleSystem> ();
		ps.Play ();
		Destroy (gameObject, ps.duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
