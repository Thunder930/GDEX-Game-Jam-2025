using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BlockPlacer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] counters;
    private int[] counts;
    public TileBase currentTile { private get; set; }
    Tilemap tilemap;

    private void Start()
    {
        tilemap = FindAnyObjectByType<Tilemap>();
        counts = new int[counters.Length];
    }

    public bool PlaceBlock()
    {
        Vector3Int location = tilemap.WorldToCell(transform.position);
        if (tilemap.GetTile(location) != null) return false;
        tilemap.SetTile(location, currentTile);
        if (tilemap.GetInstantiatedObject(location).TryGetComponent<IBlock>(out IBlock block)) {
            block.placedByPlayer = true;
        }
        return true;
    }

    public void RemoveBlock()
    {
        Vector3Int location = tilemap.WorldToCell(transform.position);
        GameObject tile = tilemap.GetInstantiatedObject(location);
        if (tile != null)
        {
            IBlock block = tile.GetComponent<IBlock>();
            if (block != null && block.placedByPlayer)
            {
                tilemap.SetTile(location, null);
            }
        }
    }

    public void IncrementCounter(int counterIndex)
    {
        counts[counterIndex]++;
        counters[counterIndex].text = "x " + counts[counterIndex];
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
    }
}