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

	private List<int[]> tilesBeingAltered = new List<int[]>();



	void Start () {
		grid = GetComponent<Tilemap> ();
	}

	void Update () {

	}


	/*
	 * Make the tile at (x, y) in the grid disappear (this method is called when the player step on this tile) 
	 */
	public IEnumerator effrite(int x, int y) {
		int[] w = {x, y};
		if (!tilesBeingAltered.Contains(w)) {
			tilesBeingAltered.Add (w);
			yield return new WaitForSeconds(timeDisappearing);
			grid.SetTile (new Vector3Int (x, y, 0), null);
			yield return new WaitForSeconds(tileRespawnTime);

			// Waiting for the player not to be on the tile (this assumes the player is 1x2)
			GameObject player = GameObject.FindWithTag ("Player");
			while (Annex.isInRange(player.transform.position.x, x-0.5f, x+1.5f) && Annex.isInRange(player.transform.position.y, y-1f, y+2f)) {		// While the player is in the zone of the tile
				yield return null;
			}
			grid.SetTile(new Vector3Int(x, y, 0), tile);
			tilesBeingAltered.Remove (w);
		}


	}

}
