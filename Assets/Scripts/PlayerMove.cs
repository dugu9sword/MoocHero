using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	Animator animator;
	Transform transform;

	// Use this for initialization
	void Start () {
		animator= GetComponent<Animator> ();
		transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.W)) 
			animator.SetBool("running",true);
		if (Input.GetKeyUp (KeyCode.W)) 
			animator.SetBool("running",false);
		*/
		if (GameManager.gm.gameState == GameManager.GameState.Playing) {
			if (Input.GetKey (KeyCode.W))
				animator.Play ("run");
			if (Input.GetKey (KeyCode.A))
				transform.Rotate (new Vector3 (0f, -5f));
			if (Input.GetKey (KeyCode.D))
				transform.Rotate (new Vector3 (0f, 5f));

			if (Input.GetKeyDown (KeyCode.Q))
				animator.Play ("die");
			if (Input.GetKeyDown (KeyCode.J))
				GetComponent<Animator> ().Play ("attack");
		}
	}
}
