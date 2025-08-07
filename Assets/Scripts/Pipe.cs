using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pipe : MonoBehaviour, ICorruptible, IPurifiable
{
    [SerializeField] bool topOpening;
    [SerializeField] bool leftOpening;
    [SerializeField] bool rightOpening;
    [SerializeField] bool bottomOpening;
    private List<Vector3Int> openings = new List<Vector3Int>();
    private Tilemap tilemap;
    private Vector3Int location;
    private int corruptionPower;
    public bool IsCorrupted => IsOpeningCorrupted();
    public float timeSincePurificationStart { get; set; }
    public bool purificationStarted { get; set; }
    public int purificationPower { get; set; }

    private void Start()
    {
        tilemap = FindAnyObjectByType<Tilemap>();
        location = tilemap.WorldToCell(transform.position);
        if (topOpening) openings.Add(location + new Vector3Int(0, 1));
        if (leftOpening) openings.Add(location + new Vector3Int(-1, 0));
        if (rightOpening) openings.Add(location + new Vector3Int(1, 0));
        if (bottomOpening) openings.Add(location + new Vector3Int(0, -1));
    }

    private void Update()
    {
        foreach (Vector3Int tilePos in openings)
        {
            GameObject tile = tilemap.GetInstantiatedObject(tilePos);
            if (tile != null)
            {
                if (tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible))
                {
                    corruptible.AddCorruptionSpeed(corruptionPower, location);
                }
                if (tile.TryGetComponent<IPurifiable>(out IPurifiable purifiable))
                {
                    purifiable.SetPurificationPower(purificationPower, location);
                }
            }
        }
    }

    public void AddCorruptionSpeed(int corruptionSpeed, Vector3Int from)
    {
        if (openings.Contains(from) && corruptionSpeed > 0)
        {
            foreach (Vector3Int opening in openings)
            {
                if (opening != from)
                {

                }
            }
        }
    }

    public void SetPurificationPower(int purificationPower, Vector3Int from)
    {
        if (openings.Contains(from))
        {
            this.purificationPower = Math.Max(this.purificationPower, purificationPower);
        }
    }

    private bool IsOpeningCorrupted()
    {
        foreach (Vector3Int opening in openings)
        {
            GameObject tile = tilemap.GetInstantiatedObject(opening);
            if (tile != null && tile.TryGetComponent<ICorruptible>(out ICorruptible corruptible) && corruptible.IsCorrupted)
            {
                return true;
            }
        }
        return false;
    }
}
