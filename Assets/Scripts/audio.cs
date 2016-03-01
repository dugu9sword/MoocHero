using UnityEngine;
using System.Collections;

public class audio : MonoBehaviour {

    bool entered = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnCollisionEnter(Collision collision)
    {
        if (!entered && collision.gameObject.tag.Equals("domino"))
        {
            //GetComponent<AudioSource>().Play();
            entered = true;
        }
    }
}
