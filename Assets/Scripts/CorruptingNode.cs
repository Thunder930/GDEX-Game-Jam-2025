using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CorruptingNode : MonoBehaviour, IPurifiable
{
    [SerializeField] TileBase inertNode;
    [SerializeField] int corruptionPower;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private Vector3Int location;
    private Tilemap tilemap;
    private int purificationPower = 0;

    private void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        location = tilemap.WorldToCell(transform.position);
        adjacentTileLocations.Add(location + new Vector3Int(-1, 0, 0));
        adjacentTileLocations.Add(location + new Vector3Int(1, 0, 0));
        adjacentTileLocations.Add(location + new Vector3Int(0, -1, 0));
        adjacentTileLocations.Add(location + new Vector3Int(0, 1, 0));
        adjacentTileLocations.Add(location + new Vector3Int(0, 0, -1));
        adjacentTileLocations.Add(location + new Vector3Int(0, 0, 1));
    }

    private void Update()
    {
        foreach (Vector3Int tilePos in adjacentTileLocations)
        {
            GameObject tile = tilemap.GetInstantiatedObject(tilePos);
            if (tile != null)
            {
                if (tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible))
                {
                    corruptible.AddCorruptionSpeed(corruptionPower);
                }
                if (tile.TryGetComponent<IPurifiable>(out IPurifiable purifiable))
                {
                    purifiable.AddPurificationPower(purificationPower - 1);
                }
            }
        }
        if (purificationPower > 0)
        {
            foreach (Vector3Int tilePos in adjacentTileLocations)
            {
                GameObject tile = tilemap.GetInstantiatedObject(tilePos);
                if (tile != null)
                {
                    if (tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible))
                    {
                        if (corruptible.IsCorrupted)
                        {
                            return;
                        }
                    }
                }
            }
            tilemap.SetTile(location, inertNode);
        }
    }

    public void AddPurificationPower(int purificationPower)
    {
        if (purificationPower > 0)
        {
            this.purificationPower = purificationPower;
        }
    }
}