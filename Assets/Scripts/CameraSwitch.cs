using UnityEngine;
using System.Collections;

public class CameraSwitch : MonoBehaviour {

    public Camera camera1,camera2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (camera2.enabled)
            {
                camera2.enabled = false;
                camera1.enabled = true;
            }
            else
            {
                camera2.enabled = true;
                camera1.enabled = false;
            }


        }
	}
}
