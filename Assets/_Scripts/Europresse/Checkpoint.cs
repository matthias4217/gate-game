using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class Checkpoint : MonoBehaviour {

	[Tooltip("")]
	public float xOffset = 0f;
	[Tooltip("The color of the flag when the checkpoint is activated")]
	public Color activatedColor;
	[Tooltip("The time it takes to do the color gradient when activated")]
	public float colorTransitionTime;

        [Tooltip("start, complete ou fail")]
        public GAProgressionStatus progressionStatus;
        [Tooltip("Le niveau")]
        public string progression01;
        [Tooltip("Le checkpoint dans le niveau")]
        public string progression02;
        //public string progression03;

	private bool isEnabled = false;
	private Vector3 colorSmoothing;

	private SpriteRenderer spriteRenderer;



	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		isEnabled = false;
	}


	void OnTriggerEnter2D (Collider2D other) {
		if (!isEnabled && other.CompareTag("Player")) {
			other.GetComponent<PlatformerPlayer> ().SetCheckpoint (this.transform.position + xOffset * Vector3.right);
			isEnabled = true;
			print ("HAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADOUKEN");
			spriteRenderer.color = activatedColor;
                        // On envoie où en est la progression
                        GameAnalytics.NewProgressionEvent(progressionStatus, progression01, progression02);
		}
	}


	[System.Obsolete("Useless and incorrect implémentation")]
	private IEnumerator ColorGradient (Color targetColor, float gradientDuration) {
		while (spriteRenderer.color != targetColor) {
			spriteRenderer.color = Annex.toColor (Vector3.SmoothDamp (Annex.toVector3 (spriteRenderer.color), Annex.toVector3 (targetColor), ref colorSmoothing, gradientDuration));
			yield return null;
		}
	}




}
