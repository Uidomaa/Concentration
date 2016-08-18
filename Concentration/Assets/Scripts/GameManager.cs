using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Transform    tilePrefab;
    public Material[]   faceMaterials;  //There must be twice as many tiles as materials!
    public Text         tilesClickedText;
    public Transform    cam;

    private int numTiles = 16;  //Currently hardcoded - make adjustable if there's time
    private int tilesMatched = 0;

	// Use this for initialization
	void Start () {
        CreateTiles();
        Concentration.Reset();
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    //Currently create 4 x 4 tiles in a grid
    void CreateTiles()
    {
        //Get positions
        List<Vector3> tilePositions = new List<Vector3>();
        for (int i = 0; i < Mathf.Sqrt(numTiles); i++)
        {
            for (int j = 0; j < Mathf.Sqrt(numTiles); j++)
            {
                tilePositions.Add(new Vector3(i * 2, 10, j * 2));
            }
        }

        //Instantiate tiles
        List<Transform> unassignedTiles = new List<Transform>();
        Transform curTile;
        for (int i = 0; i < numTiles; i++)
        {
            unassignedTiles.Add(curTile = Instantiate(tilePrefab, tilePositions[i], tilePrefab.rotation) as Transform);
            curTile.GetComponent<TileScript>().manager = this;
        }

        //Assign custom face
        List<Transform> assignedTiles = new List<Transform>();
        for (int i = 0; i < faceMaterials.Length; i++)
        {
            unassignedTiles[i * 2].GetComponent<TileScript>().SetMaterial(faceMaterials[i]);
            unassignedTiles[i * 2 + 1].GetComponent<TileScript>().SetMaterial(faceMaterials[i]);
        }

        //Shuffle tiles
        for (var i = unassignedTiles.Count - 1; i > 0; i--)
        {
            int j = (int)Mathf.Floor(Random.value * (i + 1));
            Transform temp = unassignedTiles[i];
            unassignedTiles[i] = unassignedTiles[j];
            unassignedTiles[j] = temp;
        }

        //Assign position
        Go.defaultEaseType = GoEaseType.BounceOut;
        for (int i = 0; i < numTiles; i++)
        {
            unassignedTiles[i].position = tilePositions[i];
            Go.to(unassignedTiles[i], Random.Range(1f, 2f), new GoTweenConfig().position(Vector3.up * -10f, true));
        }
    }

    //Method called when a tile is selected
    public void TileSelected(Transform tile)
    {
        //Record tile
        Concentration.TileSelected(tile);
        //Update tiles clicked display
        tilesClickedText.text = Concentration.GetTilesClicked().ToString();
        //Check if 2 tiles have been selected. If so, compare them
        if (Concentration.NumFlippedTiles() == 2)
        {
            List<Transform> theTiles = Concentration.GetTiles();
            //If the tiles match, get rid of them
            if (Concentration.CompareTiles())
            {
                Go.to(cam, 0.5f, new GoTweenConfig().shake(Vector3.one * 0.25f));
                foreach (Transform matchingTile in theTiles)
                {
                    StartCoroutine(matchingTile.GetComponent<TileScript>().Disappear(0.5f));
                }
                tilesMatched += 2;
            }
            //otherwise flip them back over
            else
            {
                foreach (Transform nonMatchingTile in theTiles)
                {
                    StartCoroutine(nonMatchingTile.GetComponent<TileScript>().FlipBackOver(1f, 0.25f));
                }
            }
            Concentration.ClearSelectedTiles();
            //Check if game is done
            if (tilesMatched == numTiles)
            {
                StartCoroutine(EndGame());
            }
        }
    }

    IEnumerator EndGame ()
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene("EndScene");
    }
}
