using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corruptible_Tile : MonoBehaviour, ICorruptible, IPurifiable, IBlock
{
    [SerializeField] CorruptingStages stages;
    [SerializeField] int corruptionPower;

    public int currentStage;
    private float timeSinceLastCorruption;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private const float TIME_TO_ADVANCE_CORRUPTION = 0.1f;

    public int purificationPower { get; set; }

    private int corruptionSpeed = 0;
    public bool purificationStarted { get; set; }
    public float timeSincePurificationStart { get; set; }

    public bool placedByPlayer { get; set; } = false;

    private const float TIME_TO_PASS_ALONG_PURIFICATION = 1.0f;

    private void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        location = tilemap.WorldToCell(transform.position);
        adjacentTileLocations.AddRange(LevelManager.GetAdjacentTileLocations(location));
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
                    corruptible.AddCorruptionSpeed(corruptionPower, location);
                }
                if (timeSincePurificationStart >= TIME_TO_PASS_ALONG_PURIFICATION && tile.TryGetComponent<IPurifiable>(out IPurifiable purifiable))
                {
                    purifiable.SetPurificationPower(purificationPower - 1, location);
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

    public void AddCorruptionSpeed(int corruptionSpeed, Vector3Int from)
    {
        if (corruptionSpeed > 0)
        {
            this.corruptionSpeed += corruptionSpeed;
        }
    }

    public void SetPurificationPower(int purificationPower, Vector3Int from)
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

    public int GetCorruptionPower(Vector3Int from)
    {
        return corruptionPower;
    }

    public bool IsCorrupted()
    {
        return currentStage > 0;
    }

    public bool IsFullyCorrupted()
    {
        return currentStage == stages.stages.Length - 1;
    }
}
