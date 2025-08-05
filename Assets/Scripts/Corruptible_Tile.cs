using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corruptible_Tile : MonoBehaviour, ICorruptible, IPurifiable
{
    [SerializeField] CorruptingStages stages;
    [SerializeField] int corruptionPower;

    public int currentStage;
    private float timeSinceLastCorruption;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private const float TIME_TO_ADVANCE_CORRUPTION = 0.1f;
    public bool IsCorrupted { get; private set; } = false;
    public int purificationPower { get; set; }

    private int corruptionSpeed = 0;
    public bool purificationStarted { get; set; }
    public float timeSincePurificationStart { get; set; }
    private const float TIME_TO_PASS_ALONG_PURIFICATION = 1.0f;

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
        if (purificationStarted)
        {
            timeSincePurificationStart = Mathf.Min(timeSincePurificationStart + Time.deltaTime, TIME_TO_PASS_ALONG_PURIFICATION); // prevent overflow
        }
        timeSinceLastCorruption = Mathf.Min(timeSinceLastCorruption + Time.deltaTime, TIME_TO_ADVANCE_CORRUPTION); // prevent overflow

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

        if (timeSinceLastCorruption >= TIME_TO_ADVANCE_CORRUPTION)
        {
            timeSinceLastCorruption = 0.0f;
            if ((corruptionSpeed - purificationPower) > 0 && currentStage < stages.stages.Length - 1)
            {
                LevelManager.ReplaceTileAndTransferProperties(location, stages.stages[currentStage + 1]);
            }
            else if ((corruptionSpeed - purificationPower) <= 0 && currentStage > 0)
            {
                LevelManager.ReplaceTileAndTransferProperties(location, stages.stages[currentStage - 1]);
            }
        }
        corruptionSpeed = 0;
    }

    public void AddCorruptionSpeed(int corruptionSpeed)
    {
        if (corruptionSpeed > 0)
        {
            this.corruptionSpeed += corruptionSpeed;
        }
    }

    public void SetPurificationPower(int purificationPower)
    {
        this.purificationPower = Math.Max(this.purificationPower, purificationPower);
        if (purificationPower > 0)
        {
            purificationStarted = true;
        } else
        {
            purificationStarted = false;
            timeSincePurificationStart = 0.0f;
        }
    }
}
