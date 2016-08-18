using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Concentration {

    private static List<Transform> tiles = new List<Transform>();
    private static int tilesClicked = 0;

    public static int NumFlippedTiles()
    {
        return tiles.Count;
    }

    //Adds material to material list
    public static void TileSelected (Transform tile)
    {
        tiles.Add(tile);
        tilesClicked++;
    }

    public static bool CompareTiles ()
    {
        //Compare face materials
        return tiles[0].GetComponent<TileScript>().tileFace.material.name.Equals(tiles[1].GetComponent<TileScript>().tileFace.material.name);
    }

    public static List<Transform> GetTiles ()
    {
        return tiles;
    }

    public static void ClearSelectedTiles()
    {
        tiles.Clear();
    }

    public static int GetTilesClicked ()
    {
        return tilesClicked;
    }

    public static void Reset ()
    {
        tiles.Clear();
        tilesClicked = 0;
    }
}
