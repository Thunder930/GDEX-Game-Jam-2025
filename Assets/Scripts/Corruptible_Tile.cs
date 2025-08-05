using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Corruptible_Tile : MonoBehaviour, ICorruptible, IPurifiable
{
    [SerializeField] CorruptingStages stages;
    [SerializeField] int corruptionPower;
    private int purificationPower = 0;

    public int currentStage;
    private float timeSinceLastCorruption;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private const float TIME_TO_ADVANCE_CORRUPTION = 0.1f;
    public bool IsCorrupted { get; private set; } = false;
    private int corruptionSpeed = 0;

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

        if (timeSinceLastCorruption >= TIME_TO_ADVANCE_CORRUPTION)
        {
            timeSinceLastCorruption = 0.0f;
            if ((corruptionSpeed - purificationPower) > 0 && currentStage < stages.stages.Length - 1)
            {
                tilemap.SetTile(location, stages.stages[currentStage + 1]);
            }
            else if ((corruptionSpeed - purificationPower) < 0 && currentStage > 0)
            {
                tilemap.SetTile(location, stages.stages[currentStage - 1]);
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

    public void AddPurificationPower(int purificationPower)
    {
        if (purificationPower > 0)
        {
            this.purificationPower = purificationPower;
        }
    }
}
