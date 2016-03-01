using UnityEngine;
using System.Collections;

public class GameSettingsController : MonoBehaviour {

	public GameObject InitSubPanel;
	public GameObject StartSubPanel;
	public GameObject OptionSubPanel;

	// Use this for initialization
	void Start () {
		ActiveInitPanel ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActiveInitPanel(){
		InitSubPanel.SetActive (true);
		StartSubPanel.SetActive (false);
		OptionSubPanel.SetActive (false);
	}

	public void ActiveStartPanel(){
		InitSubPanel.SetActive (false);
		StartSubPanel.SetActive (true);
		OptionSubPanel.SetActive (false);
	}

	public void ActiveOptionPanel(){
		InitSubPanel.SetActive (false);
		StartSubPanel.SetActive (false);
		OptionSubPanel.SetActive (true);
	}
}
