using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public int health=2;
	public int value=1;
	public float hitBackZ=1;

	public AudioClip enemyHurtAudio;
	private ParticleSystem hurt;
	// Use this for initialization
	void Start () {
		hurt = GetComponentInChildren<ParticleSystem> ();
	}

	public void TakeDamage(int damage){
		health -= damage;
		hurt.Play ();
		if (enemyHurtAudio!=null)
			AudioSource.PlayClipAtPoint (enemyHurtAudio, transform.position);
		if (health <= 0) {
			if (GameManager.gm != null) {
				GameManager.gm.AddScore (value);
			}
			gameObject.GetComponent<Animator> ().SetTrigger ("isDead");
			gameObject.GetComponent<Rigidbody> ().useGravity = false;
			gameObject.GetComponent<Collider> ().enabled = false;
			Destroy (gameObject, 3.0f);
		} else
			transform.Translate (0, 0, -hitBackZ);
	}
}
