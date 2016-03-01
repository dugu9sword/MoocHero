using UnityEngine;
using System.Collections;

public class EnemyTrace : MonoBehaviour {

	public GameObject target;
	public float moveSpeed=3.0f;
	public float minDist=1.0f;
	public float maxDist=10.0f;

	/*private float minTimeRandWalk=2.0f;
	private float maxTimeRandWalk=5.0f;
	private float timeRandWalk;
	private float timer;*/

	private float dist;
	private Rigidbody rb;
	private Animator anim;
	private EnemyHealth enemyHealth;
	// Use this for initialization
	void Start () {
		if (target == null || target.tag!="Player") {
			target = GameObject.FindGameObjectWithTag ("Player");
		}
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
		//Invoke ("SetConstraints", 2);
	}

	void SetConstraints(){
		GetComponent<Rigidbody> ().constraints = 
			RigidbodyConstraints.FreezePositionY |
			RigidbodyConstraints.FreezeRotationX |
			RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth.health <= 0)
			return;
		if (target == null)
			return;
		dist = Vector3.Distance (transform.position, target.transform.position);
		
		if (GameManager.gm.gameState == GameManager.GameState.Playing) {
			if (dist > minDist) TraceTarget ();
			anim.SetBool ("isStop", false);
		}else if (GameManager.gm.gameState == GameManager.GameState.GameOver) {
			anim.SetTrigger ("isPlayerDead");
		}
	}
	private void TraceTarget(){
		transform.LookAt (target.transform);
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}
	/*private void RotateRandomly(){
		float randAngle = Random.Range (0.0f, 360.0f);
		transform.Rotate (0, 0, randAngle);
	}*/
}
