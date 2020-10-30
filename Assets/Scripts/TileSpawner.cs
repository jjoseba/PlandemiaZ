using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{

    public Transform player;
    public GameObject startingTile;
    public GameObject[] tilePrefabs;

    public GameObject tileGatesPrefab;
    public int numTileGatesAppearance = 6;
    public static int startingTiles = 2;

    private List<GameObject> spawnedTiles = new List<GameObject>();
    public float tileLength = 20;
    public int visibleTiles = 5;
    public int currentTile = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTile = -visibleTiles;
        for(int i=0; i<visibleTiles; i++)
        {
            SpawnTile(startingTile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z / tileLength > currentTile - 1)
        {
            SpawnTile(tilePrefabs[Random.Range(0, tilePrefabs.Length)]);
            DeleteTile();
        }
    }

    void SpawnTile(GameObject tilePrefab)
    {

        GameObject instance = tilePrefab;
        if (currentTile < startingTiles) instance = startingTile;
        // If we are in the Gates tile number, we instantiate it instead
        if (currentTile == numTileGatesAppearance) instance = tileGatesPrefab;

            GameObject tile = Instantiate(instance, 
                transform.forward * currentTile * tileLength, transform.rotation);
        spawnedTiles.Add(tile);
        currentTile++;
        LevelManager.level = currentTile;
    }

    private void DeleteTile()
    {
        Destroy(spawnedTiles[0]);
        spawnedTiles.RemoveAt(0);
    }
}
