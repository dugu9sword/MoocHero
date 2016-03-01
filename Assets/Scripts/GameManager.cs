using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	static public GameManager gm;

	private AudioListener audioListener;

	public GameObject player;
	public int TargetScore = 5;
	public enum GameState {Playing,GameOver,Winning};
	public GameState gameState;

	public GameObject playingCanvas;
	public Text scoreText;
	public Text timeText;
	public Slider healthSlider;
	public Image hurtImage;

	public AudioClip gameWinAudio;
	public AudioClip gameOverAudio;
	//public GameObject winningCanvas;
	//public GameObject gameOverCanvas;
	public GameObject gameResultCanvas;
	public GameObject mobileControlRigCanvas;

	/*public Text gameResultText;
	public Text userNameText;
	public Text userScoreText;
	public Text userTimeText;*/

	public GameObject firstUserText;
	public GameObject secondUserText;
	public GameObject thirdUserText;
	public GameObject userText;
	public Text gameMessage;

	private int currentScore;
	private float startTime;
	private float currentTime;
	private PlayerHealth playerHealth;

	private bool cursor;
	private float sound;
	private Color flashColor = new Color (1.0f, 0.0f, 0.0f, 0.3f);
	private float flashSpeed = 2.0f;

	private UserData firstUserData;
	private UserData secondUserData;
	private UserData thirdUserData;
	private UserData currentUserData;
	private UserData[] userDataArray = new UserData[4];

	private bool isPlayGameAudio=false;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		if (gm == null)
			gm = GetComponent<GameManager> ();
		if (player == null)
			player = GameObject.FindGameObjectWithTag ("Player");
		audioListener = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<AudioListener> ();
		
		currentScore = 0;
		startTime = Time.time;

		playerHealth = player.GetComponent<PlayerHealth> ();
		if (playerHealth) {
			healthSlider.maxValue = playerHealth.startHealth;
			healthSlider.minValue = 0;
			healthSlider.value = playerHealth.currentHealth;
		}

		//gameOverCanvas.SetActive (false);
		//winningCanvas.SetActive (false);
		gameResultCanvas.SetActive(false);
		playingCanvas.SetActive (true);

		if(PlayerPrefs.GetString("Username")=="")
			PlayerPrefs.SetString("Username","无名英雄");
		if (PlayerPrefs.GetString ("FirstUser") != "") {
			firstUserData = new UserData (PlayerPrefs.GetString ("FirstUser"));
		} else
			firstUserData = new UserData ();
		if (PlayerPrefs.GetString ("SecondUser") != "") {
			secondUserData = new UserData (PlayerPrefs.GetString ("SecondUser"));
		} else
			secondUserData = new UserData ();
		if (PlayerPrefs.GetString ("ThirdUser") != "") {
			thirdUserData = new UserData (PlayerPrefs.GetString ("ThirdUser"));
		} else
			thirdUserData = new UserData ();

		audioListener.enabled = (PlayerPrefs.GetInt ("Sound") == 1);
	}
	
	// Update is called once per frame
	void Update () {
		hurtImage.color = Color.Lerp (
			hurtImage.color, 
			Color.clear, 
			flashSpeed * Time.deltaTime
		);

		switch (gameState) {

		case GameState.Playing:
			if (Input.GetKeyDown (KeyCode.Escape))
				Cursor.visible = !Cursor.visible;
			if(playerHealth){
				if (playerHealth.isAlive == false)
					gm.gameState = GameState.GameOver;
				else if (currentScore >= TargetScore) {
					currentScore = TargetScore;
					gm.gameState = GameState.Winning;
				}
				else {
					scoreText.text = "灭 敌 战 绩 ： " + currentScore;
					healthSlider.value = gm.playerHealth.currentHealth;				
				}
				currentTime = Time.time - startTime;
				timeText.text = "战 斗 时 间 ： " + currentTime.ToString ("0.00");
				mobileControlRigCanvas.SetActive (true);
			}
			break;
		case GameState.Winning:
			if (!isPlayGameAudio) {
				AudioSource.PlayClipAtPoint (gameWinAudio, player.transform.position);
				Cursor.visible = true;
				playingCanvas.SetActive (false);
				gameResultCanvas.SetActive (true);
				mobileControlRigCanvas.SetActive (false);
				//winningCanvas.SetActive (true);
				isPlayGameAudio = true;
				EditGameOverCanvas();
			}
			break;
		case GameState.GameOver:
			if (!isPlayGameAudio) {
				AudioSource.PlayClipAtPoint (gameOverAudio, player.transform.position);
				Cursor.visible = true;
				playingCanvas.SetActive (false);
				gameResultCanvas.SetActive (true);
				mobileControlRigCanvas.SetActive (false);
				//gameOverCanvas.SetActive (true);
				isPlayGameAudio = true;
				EditGameOverCanvas();
			}
			break;
		}
	}


	void EditGameOverCanvas(){
		/*if (gm.gameState == GameState.GameOver)
			gameResultText.text = "GAME OVER";
		else if(gm.gameState==GameState.Winning)
			gameResultText.text="YOU WIN";
		userNameText.text=PlayerPrefs.GetString("Username");
		userScoreText.text = currentScore.ToString();
		userTimeText.text = currentTime.ToString("0.00");*/

		//Debug.Log (userNameText.text + " 0 " + userScoreText.text + " " + userTimeText.text);
		currentUserData = new UserData (PlayerPrefs.GetString("Username") + " 0 " + currentScore.ToString() + " " + currentTime.ToString("0.00"));
		currentUserData.isUser = true;
		userDataArray [0] = currentUserData;
		int arrayLength = 1;
		if (firstUserData.order != "0")
			userDataArray [arrayLength++] = firstUserData;
		if (secondUserData.order != "0")
			userDataArray [arrayLength++] = secondUserData;
		if (thirdUserData.order != "0")
			userDataArray [arrayLength++] = thirdUserData;

		//Debug.Log (arrayLength);
		mySort (arrayLength);
		foreach (UserData i in userDataArray) {
			if (i.isUser == true) {
				currentUserData = i;
				break;
			}
		}

		switch (currentUserData.order) {
		case "1":
			gameMessage.text = "恭喜你荣登慕课英雄榜榜首！";
			break;
		case "2":
			gameMessage.text = "恭喜你荣登慕课英雄榜榜眼！";
			break;
		case "3":
			gameMessage.text = "恭喜你荣登慕课英雄榜探花！";
			break;
		default:
			gameMessage.text = "";
			break;
		}

		Text[] texts;
		if (arrayLength > 0) {
			PlayerPrefs.SetString ("FirstUser", userDataArray [0].DataToString ());
			texts = firstUserText.GetComponentsInChildren<Text> ();
			LeaderBoardChange(texts,userDataArray [0]);
			arrayLength--;
		}
		if (arrayLength > 0) {
			PlayerPrefs.SetString ("SecondUser", userDataArray [1].DataToString ());
			texts = secondUserText.GetComponentsInChildren<Text> ();
			LeaderBoardChange(texts,userDataArray [1]);
			arrayLength--;
		}
		if (arrayLength > 0) {
			PlayerPrefs.SetString ("ThirdUser", userDataArray [2].DataToString ());
			texts = thirdUserText.GetComponentsInChildren<Text> ();
			LeaderBoardChange(texts,userDataArray [2]);
			arrayLength--;
		}

		if (currentUserData.order != "1" && currentUserData.order != "2" && currentUserData.order != "3") {
			texts = userText.GetComponentsInChildren<Text> ();
			LeaderBoardChange (texts, currentUserData);
		} else {
			userText.SetActive (false);
		}

	}	

	void mySort(int arrayLength){
		UserData temp;
		for (int i = 0; i < arrayLength; i++) {
			for (int j = i+1; j < arrayLength; j++) {
				if (userDataArray [i] < userDataArray [j]) {
					temp = userDataArray [j];
					userDataArray [j] = userDataArray [i];
					userDataArray [i] = temp;
				}
			}
		}

		for (int i = 0; i < arrayLength; i++)
			userDataArray [i].order = (i + 1).ToString();
	}

	void LeaderBoardChange(Text[] texts,UserData data){
		//texts [0].text = data.order.ToString();
		texts [0].text = data.username;
		texts [1].text = data.score.ToString();
		texts [2].text = data.time.ToString();
		if (data.isUser) {
			//texts [0].fontStyle = FontStyle.Bold;
			texts [0].fontStyle = FontStyle.Bold;
			texts [1].fontStyle = FontStyle.Bold;
			texts [2].fontStyle = FontStyle.Bold;
		}
	}

	public void AddScore(int value){
		currentScore += value;
	}

	public void PlayerTakeDamage(int value){
		if (playerHealth != null)
			playerHealth.TakeDamage(value);
		hurtImage.color = flashColor;
	}

	public void PlayAgain(){
		SceneManager.LoadScene("PJ3");
	}
	public void BackToMain(){
		SceneManager.LoadScene("Main");
	}

	public void PlayerAddHealth(int value){
		if (playerHealth != null)
			playerHealth.AddHealth(value);
	}
}
