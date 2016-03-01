using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverEffects : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 direction;
    private GameObject player;
    private GameObject[] enemies;
	private GameObject[] gameoverEnemies;

	public Image darkImage;
	public GameObject enemyPrefab;


    private bool initialized = false;
	private bool gameover = false;

	private bool rankingPanelActive = false;

	public GameObject rankingPanel;

	public static bool cameraSwtichCompleted=false;

    // Use this for initialization
    void Start()
    {

    }

    void Init()
    {
        if (!initialized)
        {
			player = GameObject.FindGameObjectWithTag("Player");
			enemies = GameObject.FindGameObjectsWithTag("Enemy");
            
            direction = new Vector3(3.0f, 3.0f, 5.0f);

            Camera gameOverCamera = GameObject.Find("GameOverCamera").GetComponent<Camera>();
            cameraTransform = gameOverCamera.transform;

            cameraTransform.position = Camera.main.transform.position;
            cameraTransform.eulerAngles = Camera.main.transform.eulerAngles;

			Camera.main.enabled = false;
            gameOverCamera.enabled = true;

            initialized = true;
        }
    }

	void CameraSwitch(bool win)
    {
		//rankingPanel.SetActive ((cameraTransform.position-(player.transform.position + direction)).sqrMagnitude<1.0f);
		if (!win) {
			rankingPanel.SetActive (rankingPanelActive);
			Invoke ("enablePanel", 3);
		}
		cameraTransform.position = Vector3.Lerp(
			cameraTransform.position, 
            player.transform.position + direction, 
            0.01f
		);
        cameraTransform.LookAt(player.transform);
    }

	void enablePanel(){
		rankingPanelActive = true;
	}


    // Update is called once per frame
    void Update()
    {
        switch (GameManager.gm.gameState)
        {
            case GameManager.GameState.Playing:
                return;
		case GameManager.GameState.Winning:
			Init ();
			CameraSwitch (true);
			foreach (GameObject enemy in enemies)
				Destroy (enemy);
			GameObject.Destroy (GameObject.Find ("Gun"));
            break;
		case GameManager.GameState.GameOver:
			Init ();
			CameraSwitch (false);
			GameObject.Destroy (GameObject.Find ("Gun"));
			if (!gameover) {

				gameover = true;
				darkImage.color = Color.black;
				foreach (GameObject enemy in enemies)
					GameObject.Destroy (enemy);
				Vector3 enemyCenter = new Vector3 (
					player.transform.position.x - direction.x,
					player.transform.position.y,
					player.transform.position.z - direction.z);
				Vector3 enemyVector = new Vector3 (direction.z, 0, -direction.x);
				enemyVector.Normalize ();
				gameoverEnemies = new GameObject[7];

				for (int i = -3; i <= 3; i++) {
					/*
					Vector3 randomVector = new Vector3 (
						Random.Range (-0.5f, 0.5f), 
						0, 
						Random.Range (-0.5f, 0.5f));
						*/
					GameObject _enemy = (GameObject)GameObject.Instantiate (
						enemyPrefab,
						enemyCenter + i * enemyVector * 1.5f,
						Quaternion.identity);
					_enemy.transform.LookAt (player.transform.position + direction);
					gameoverEnemies [i + 3] = _enemy;
				}
			}

			darkImage.color = Color.Lerp (darkImage.color, Color.clear, 0.2f * Time.deltaTime);
			foreach (GameObject enemy in gameoverEnemies) {
				enemy.GetComponent<Rigidbody> ().constraints =
					RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
				enemy.transform.LookAt (player.transform.position + direction);
			}
            break;

        }
    }
}
