using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour {

	private float sound;
	public InputField username;
	public Toggle soundToggle;

	public void StartGame(){
		PlayerPrefs.SetString ("Username", username.text);
		SceneManager.LoadScene("PJ3");
	}

	public void SwitchSound(){
		if (soundToggle.isOn)
			PlayerPrefs.SetInt ("Sound", 1);
		else
			PlayerPrefs.SetInt ("Sound", 0);
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
