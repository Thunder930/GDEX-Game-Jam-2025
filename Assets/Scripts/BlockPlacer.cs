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
        return true;
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