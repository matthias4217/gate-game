using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlatformerPlayer))]
public class PlatformerPlayerInput : MonoBehaviour {

	PlatformerPlayer player;

	void Start () {
		player = GetComponent<PlatformerPlayer> ();
	}

	void Update () {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown("Jump")) {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp ("Jump")) {
			player.OnJumpInputUp ();
		}
	}
}
