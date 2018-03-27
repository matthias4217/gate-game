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

		if (Input.GetButtonDown ("Jump") || Input.GetButtonDown ("Vertical"))
		{
			playerController.OnJumpInputDown ();
		}
		if (Input.GetButtonUp ("Jump") || Input.GetButtonUp ("Vertical")) 
		{
			playerController.OnJumpInputUp ();
		}
	}


}
