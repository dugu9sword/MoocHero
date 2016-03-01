using UnityEngine;
using System.Collections;

public class SwordAttack : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag.Equals ("Enemy")) {
			collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
		}
	}
}
