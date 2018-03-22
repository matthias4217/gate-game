using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlatformerPlayer))]
public class PlatformerPlayerInput : MonoBehaviour {

	PlatformerPlayer playerController;

	void Start () 
	{
		playerController = GetComponent<PlatformerPlayer> ();
	}

	void Update () 
	{
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		playerController.SetDirectionalInput (directionalInput);

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			playerController.OnJumpInputDown ();
		}
		if (Input.GetKeyUp (KeyCode.Space)) 
		{
			playerController.OnJumpInputUp ();
		}
	}
}
