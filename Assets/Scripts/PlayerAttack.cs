using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour {

	public AudioClip shootAudio;
	public float shootRange = 50.0f;

	private LineRenderer gunLine;

	public GameObject sparkPrefab;
	public GameObject _laserspark;

	private Ray ray;
	private RaycastHit hit;

	private static float LINE_RENDERER_START=0.01f;
	private static float LINE_RENDERER_END=0.35f;

	// Use this for initialization
	void Start () {
		gunLine = GetComponent<LineRenderer> ();
		gunLine.enabled = false;
	}

	// Update is called once per frame
	void Update () {
		
		bool isShooting=false;
		switch (Application.platform){
		case RuntimePlatform.WindowsEditor:
			isShooting = Input.GetMouseButtonDown (0);
			break;
		case RuntimePlatform.Android:
			isShooting = CrossPlatformInputManager.GetButtonDown ("Shoot");
			break;
		}
		if (isShooting && GameManager.gm.gameState == GameManager.GameState.Playing) {
			Shoot ();
		} else {
			gunLine.enabled = false;
		}
	}

	void Shoot()
	{
		
		AudioSource.PlayClipAtPoint (shootAudio, transform.position);
		ray.origin = transform.position;
		ray.direction = Camera.main.transform.forward;
		gunLine.enabled = true;
		gunLine.SetPosition (0, transform.position);
		if (Physics.Raycast (ray, out hit, shootRange)) {
			if (hit.transform.gameObject.tag.Equals ("Enemy")) {
				EnemyHealth enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth> ();
				if (enemyHealth.health > 0)
					enemyHealth.TakeDamage (1);
				gunLine.SetPosition (1, hit.point);
				//spark.transform.position = hit.point;
				//spark.Play ();
				gunLine.SetWidth (LINE_RENDERER_START, 
					Mathf.Clamp((hit.point - ray.origin).magnitude/shootRange,LINE_RENDERER_START,LINE_RENDERER_END));
			} else {
				gunLine.SetPosition (1, ray.origin + ray.direction * shootRange);
				gunLine.SetWidth (LINE_RENDERER_START, LINE_RENDERER_END);
			}
		} else {
			gunLine.SetPosition (1, ray.origin + ray.direction * shootRange);
			gunLine.SetWidth (LINE_RENDERER_START, LINE_RENDERER_END);
		}
		Instantiate (sparkPrefab, _laserspark.transform.position, Quaternion.identity);

	}
}
