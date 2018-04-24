using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	[Tooltip("The time it requires to fully open the door")]
	public float timeToOpen;

	private static float yTranslation = 7f;		// The vertical distance that the door goes down




	/* Open the door */
	public IEnumerator Open() {
		float initialY = transform.position.y;
		float x = transform.position.x;
		float t = 0f;
		while (t < timeToOpen) {		// Pas beau =(
			t += Time.deltaTime;
			float newY = Mathf.Clamp(initialY - yTranslation * Annex.SmoothStep(t / timeToOpen), initialY - yTranslation, initialY);
			transform.position = new Vector3(x, newY, 0);
			yield return null;
		}
		print ("Door open");
	}



}
