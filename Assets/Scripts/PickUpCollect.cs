using UnityEngine;
using System.Collections;

public class PickUpCollect: MonoBehaviour {


	public int value = 2;
	public AudioClip collectedAudio;

	void OnTriggerEnter(Collider collider){
		if (collider.gameObject.tag == "Player") {
			if (GameManager.gm != null)
				GameManager.gm.PlayerAddHealth(value);
			if (collectedAudio!=null)
				AudioSource.PlayClipAtPoint (collectedAudio, transform.position);
			Destroy(gameObject);
		}
	}
}
