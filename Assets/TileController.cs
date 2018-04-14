using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour {

	public Vector3Int endingTile = new Vector3Int(0, -15, 0);		// The tile removed which make the ending		// @
	public Tile tileToPutBack;			// A reference to the tile object which will reform the wall



	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag ("Player")) {
			Tilemap tilemap = GameObject.Find ("Tilemap").GetComponent<Tilemap> ();
			tilemap.SetTile (endingTile, tileToPutBack);
		}

	}

}