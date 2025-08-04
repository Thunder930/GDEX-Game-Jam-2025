using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corruptible_Tile : MonoBehaviour, ICorruptible, IPurifiable
{
    [SerializeField] CorruptingStages stages;
    public int currentStage;
    private float timeSinceLastCorruption;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private const float TIME_TO_ADVANCE_CORRUPTION = 0.1f;
    public bool IsCorrupted { get; private set; } = false;

    private void Start()
    {
        if (currentStage == stages.stages.Length - 1)
        {
            IsCorrupted = true;
        }
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
        timeSinceLastCorruption = Mathf.Min(timeSinceLastCorruption + Time.deltaTime, TIME_TO_ADVANCE_CORRUPTION); // prevent overflow
        if (IsCorrupted)
        {
            foreach (Vector3Int tilePos in adjacentTileLocations)
            {
                GameObject tile = tilemap.GetInstantiatedObject(tilePos);
                if (tile != null)
                {
                    if (tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible))
                    {
                        corruptible.Corrupt();
                    }
                }
            }
        }
    }

    public void Corrupt()
    {
        if (!IsCorrupted && timeSinceLastCorruption >= TIME_TO_ADVANCE_CORRUPTION)
        {
            tilemap.SetTile(location, stages.stages[currentStage + 1]);
        }
    }

    public void Purify()
    {
        tilemap.SetTile(location, stages.stages[0]);
        IsCorrupted = false;
    }
}
