using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FriableTile : MonoBehaviour {

	[Tooltip("The amount of time it requires to make a tile disappears after stepped on")]
	public float timeDisappearing;
	[Tooltip("The amount of time it take for the tile to reappear")]
	public float tileRespawnTime;

	public Tile tile;

	Tilemap grid;



	void Start () {
		grid = GetComponent<Tilemap> ();
	}

	void Update () {

	}


	/*
	 * Make the tile at (x, y) in the grid disappear (this method is called when the player step on this tile) 
	 */
	public IEnumerator effrite(int x, int y) {
		yield return new WaitForSeconds(timeDisappearing);
		grid.SetTile (new Vector3Int (x, y, 0), null);
		yield return new WaitForSeconds(tileRespawnTime);
		grid.SetTile(new Vector3Int(x, y, 0), tile);


	}

}
