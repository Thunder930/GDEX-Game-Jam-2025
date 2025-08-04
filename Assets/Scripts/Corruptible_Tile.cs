using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corruptible_Tile : MonoBehaviour
{
    [SerializeField] TileBase nextStage;
    private float timeSinceLastCorruption;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTiles = new List<Vector3Int>();
    private const float TIME_TO_ADVANCE_CORRUPTION = 0.1f;

    private void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        location = tilemap.WorldToCell(transform.position);
        adjacentTiles.Add(location + new Vector3Int(-1, 0, 0));
        adjacentTiles.Add(location + new Vector3Int(1, 0, 0));
        adjacentTiles.Add(location + new Vector3Int(0, -1, 0));
        adjacentTiles.Add(location + new Vector3Int(0, 1, 0));
        adjacentTiles.Add(location + new Vector3Int(0, 0, -1));
        adjacentTiles.Add(location + new Vector3Int(0, 0, 1));
    }

    private void Update()
    {
        timeSinceLastCorruption = Mathf.Min(timeSinceLastCorruption + Time.deltaTime, TIME_TO_ADVANCE_CORRUPTION); // prevent overflow
        if (nextStage == null)
        {
            foreach (Vector3Int tilePos in adjacentTiles)
            {
                GameObject tile = tilemap.GetInstantiatedObject(tilePos);
                if (tile != null && tile.TryGetComponent<Corruptible_Tile>(out Corruptible_Tile coruptableTile))
                {
                    coruptableTile.Corrupt();
                }
            }
        }
    }

    public void Corrupt()
    {
        if (nextStage != null && timeSinceLastCorruption >= TIME_TO_ADVANCE_CORRUPTION)
        {
            tilemap.SetTile(location, nextStage);
        }
    }
}
