using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public int startHealth = 10;
	public int currentHealth;
	public bool isAlive = true;
	// Use this for initialization
	void Start () {
		currentHealth = startHealth;
		if (currentHealth > 0)isAlive = true;
		else isAlive = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentHealth <= 0) {
			isAlive = false;
		}
	}

	public void TakeDamage(int damage){
		currentHealth -= damage;
		if (currentHealth < 0)
			currentHealth = 0;
	}

	public void AddHealth(int value){
		currentHealth += value;
		if (currentHealth > startHealth)
			currentHealth = startHealth;
	}
}
