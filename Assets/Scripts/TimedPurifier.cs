using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TimedPurifier : MonoBehaviour
{
    public int purificationPower;
    private Vector3Int location;
    private Tilemap tilemap;
    private List<Vector3Int> adjacentTileLocations = new List<Vector3Int>();
    private float timeSincePlaced = 0.0f;
    private float timeSinceLastStage;
    private const float TIME_TO_PASS_ALONG_PURIFICATION = 1.0f;
    private const float TIME_TO_ADVANCE_STAGE = 3.0f;
    [SerializeField] TimedPurificationStages stages;
    [SerializeField] int currentStage;

    private void Start()
    {
        tilemap = GetComponentInParent<Tilemap>();
        location = tilemap.WorldToCell(transform.position);
        adjacentTileLocations.AddRange(LevelManager.GetAdjacentTileLocations(location));
    }

    private void Update()
    {
        timeSincePlaced = Mathf.Min(timeSincePlaced + Time.deltaTime, TIME_TO_PASS_ALONG_PURIFICATION); // prevent overflow
        timeSinceLastStage = Mathf.Min(timeSinceLastStage + Time.deltaTime, TIME_TO_ADVANCE_STAGE); // prevent overflow
        if (timeSincePlaced >= TIME_TO_PASS_ALONG_PURIFICATION)
        {
            foreach (Vector3Int tilePos in adjacentTileLocations)
            {
                GameObject tile = tilemap.GetInstantiatedObject(tilePos);
                if (tile != null)
                {
                    if (tile.TryGetComponent<IPurifiable>(out IPurifiable purifiable))
                    {
                        purifiable.SetPurificationPower(purificationPower, location);
                    }
                }
            }
        }
        if (timeSinceLastStage >= TIME_TO_ADVANCE_STAGE && currentStage < stages.stages.Length - 1)
        {
            timeSinceLastStage = 0.0f;
            tilemap.SetTile(location, stages.stages[currentStage + 1]);
        }
    }
}