using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;

    [SerializeField]
    private List<TileData> tileDatas;

    private Dictionary<TileBase, TileData> dataFromTiles;

    private void Awake()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in tileDatas)
        {
            foreach (var tile in tileData.tiles)
            {
                if (tile == null)
                    continue;
                dataFromTiles.Add(tile, tileData);
            }
        }

        // Debug.Log(dataFromTiles.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int gridPosition = map.WorldToCell(mousePosition);

            TileBase clickedTile = map.GetTile(gridPosition);

            string desc = dataFromTiles[clickedTile].description;

            print("At position " + gridPosition + " there is a " + desc);
        }
    }

    public string GetTileDescription(Vector2 worldPosition)
    {
        Vector3Int gridPosition = map.WorldToCell(worldPosition);

        TileBase tile = map.GetTile(gridPosition);

        if (tile == null)
            return "No tile data";

        string desc = dataFromTiles[tile].description;

        return desc;
    }

    public bool GetTileDanger(Vector2 worldPosition)
    {
        // Vector3Int gridPosition = map.WorldToCell(worldPosition);

        // TileBase tile = map.GetTile(gridPosition);

        // if (tile == null)
        //     return false;

        // bool dangerous = dataFromTiles[tile].dangerous;

        // return dangerous;
        return false;
    }
}
