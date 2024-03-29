﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FriableTile : MonoBehaviour {

	[Tooltip("The amount of time it requires to make a tile disappears after stepped on")]
	public float timeDisappearing;
	[Tooltip("The amount of time it take for the tile to start reappearing")]
	public float tileRespawnTime;
	[Tooltip("The amount of time it takes for the tile to become visible after being reenabled")]
	public float timeReappearing;

	public Tile friableTile;

	Tilemap grid;

	private TileList tilesCurrentlyAltered;



	void Start () {
		grid = GetComponent<Tilemap> ();
		tilesCurrentlyAltered.list = new List<int[]> ();
	}

	void Update () {

	}


	/*
	 * Make the tile at (x, y) in the grid disappear (this method is called when the player step on this tile)
	 */
	public IEnumerator effrite(int x, int y) {		// DEGUEULASSE
		int[] w = {x, y};
		if (!tilesCurrentlyAltered.Contains(w)) {
			print ("Starting the coroutine");
			tilesCurrentlyAltered.Add(w);

			Vector3Int tilePosition = new Vector3Int (x, y, 0);

			// Making the tile disappear
			/*
			while (grid.GetColor(tilePosition).a > 0f) {		// while the tile is not transparent
				Color tmpColor = grid.GetColor(tilePosition);
				tmpColor.a = Mathf.Clamp (tmpColor.a, 0, 1);
				tmpColor.a -= Time.deltaTime / timeDisappearing;
				grid.SetColor(tilePosition, tmpColor);		// XXX
				yield return null;
			}
			*/

			yield return new WaitForSeconds (timeDisappearing);

			grid.SetTile (tilePosition, null);		// Disabling the tile

			yield return new WaitForSeconds (tileRespawnTime);

			// Waiting for the player not to be on the tile (this assumes the player is 1x2)
			GameObject player = GameObject.FindWithTag ("Player");
			while (Annex.isInRange(player.transform.position.x, x-0.5f, x+1.5f) && Annex.isInRange(player.transform.position.y, y-1f, y+2f)) {		// while the player is in the zone of the tile
				yield return null;
			}

			grid.SetTile(tilePosition, friableTile);		// Reenabling the tile
			// Making the tile reappear
			/*
			while (grid.GetColor(tilePosition).a < 1f) {		// while the tile is not fully opaque
				Color tmpColor = grid.GetColor (tilePosition);
				tmpColor.a += Time.deltaTime / timeReappearing;
				grid.SetColor(tilePosition, tmpColor);
				yield return null;
			}
			*/

			tilesCurrentlyAltered.Remove(w);
		}


	}

}



struct TileList {

	public List<int[]> list;

	public void Add(int[] tile) {
		/* Add the tile to the list and return its index */
		list.Add (tile);
	}

	public bool Contains(int[] tile) {
		foreach (int[] curr in list) {
			if ((curr[0] == tile[0]) && (curr[1] == tile[1])) {
				return true;
			}
		}
		return false;
	}

	public void Remove(int[] tile) {
		for (int i = 0; i < list.Count; i++) {
			int[] curr = list [i];
			if ((curr[0] == tile[0] && curr[1] == tile[1])) {
				list.RemoveAt (i);
			}
		}
	}

}
