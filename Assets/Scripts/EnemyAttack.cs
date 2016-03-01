using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public int damage=1;
	public float timeBetweenAttack=0.5f;

	public float maxScale=0.6f;
	public float minScale=0.3f;
	
	private float timer;
	private float timeTemp;
	private bool isAttack;
	private Animator anim;
	private EnemyHealth enemyHealth;
	public AudioClip enemyAttackAudio;

	void Start(){
		timer = 0.0f;
		isAttack = false;
		timeTemp=timeBetweenAttack/2;
		anim = GetComponent<Animator> ();
		enemyHealth = GetComponent<EnemyHealth> ();
	}

	/*void OnTriggerEnter(Collider collider){
		if (timer>=timeBetweenAttack && collider.gameObject.tag == "Player") {
			if(GameManager.gm!=null){
				timer=0.0f;
				isAttack=true;
				if(enemyAttackAudio!=null)
					AudioSource.PlayClipAtPoint(enemyAttackAudio,transform.position);
				GameManager.gm.PlayerTakeDamage(damage);
			}
		}
	}*/

	void OnTriggerStay(Collider collider){
		if (enemyHealth.health <= 0)
			return;
		if (timer>=timeBetweenAttack && collider.gameObject.tag == "Player") {
			if(GameManager.gm!=null && GameManager.gm.gameState==GameManager.GameState.Playing){
				timer=0.0f;
				isAttack=true;
				anim.SetBool ("isAttack", true);
				if(enemyAttackAudio!=null)
					AudioSource.PlayClipAtPoint(enemyAttackAudio,transform.position);
				GameManager.gm.PlayerTakeDamage(damage);
			}
		}
	}
	void OnTriggerExit(Collider collider){
		anim.SetBool ("isAttack", false);
	}

	void Update(){
		timer += Time.deltaTime;
		if (isAttack == true && timer > timeTemp && timer > timeBetweenAttack) {
			isAttack = false;
		}

	}
}
