using UnityEngine;
using System.Collections;

public class AutoCreateObject : MonoBehaviour {

	public GameObject createGameObject;
	public float minSeconds=5.0f;
	public float maxSeconds=10.0f;
	public GameObject targetTrace;

	private float time;
	private float createTime;
	// Use this for initialization
	void Start () {
		if(targetTrace==null)
			targetTrace=GameObject.FindGameObjectWithTag("Player");

		time = 0.0f;
		createTime = Random.Range (minSeconds, maxSeconds);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm != null && GameManager.gm.gameState != GameManager.GameState.Playing)
			return;
		time += Time.deltaTime;
		if (time >= createTime) {
			CreateObject ();
			time = 0.0f;
			createTime = Random.Range (minSeconds, maxSeconds);
		}
	}

	void CreateObject(){
		Vector3 delta = new Vector3 (0.0f, 5.0f, 0.0f);
		GameObject newGameObject = Instantiate (createGameObject, transform.position-delta, transform.rotation) as GameObject;
		if (newGameObject.GetComponent<EnemyTrace> () != null)
			newGameObject.GetComponent<EnemyTrace> ().target = targetTrace;
	}
}
