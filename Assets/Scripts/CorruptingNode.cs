using System;
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
    public bool purificationStarted { get; set; }
    public float timeSincePurificationStart { get; set; }
    private const float TIME_TO_PASS_ALONG_PURIFICATION = 1.0f;

    public int purificationPower { get; set; }

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
        if (purificationStarted)
        {
            timeSincePurificationStart = Mathf.Min(timeSincePurificationStart + Time.deltaTime, TIME_TO_PASS_ALONG_PURIFICATION); // prevent overflow
        }
        foreach (Vector3Int tilePos in adjacentTileLocations)
        {
            GameObject tile = tilemap.GetInstantiatedObject(tilePos);
            if (tile != null)
            {
                if (tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible))
                {
                    corruptible.AddCorruptionSpeed(corruptionPower);
                }
                if (timeSincePurificationStart >= TIME_TO_PASS_ALONG_PURIFICATION && tile.TryGetComponent<IPurifiable>(out IPurifiable purifiable))
                {
                    purifiable.SetPurificationPower(purificationPower - 1);
                }
            }
        }
        if (purificationPower > 0 && timeSincePurificationStart >= TIME_TO_PASS_ALONG_PURIFICATION)
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

    public void SetPurificationPower(int purificationPower)
    {
        this.purificationPower = Math.Max(this.purificationPower, purificationPower);
        if (purificationPower > 0)
        {
            purificationStarted = true;
        }
        else
        {
            purificationStarted = false;
            timeSincePurificationStart = 0.0f;
        }
    }
}