using UnityEngine;

public interface ICorruptible
{
    public int GetCorruptionPower(Vector3Int from);
    public bool IsCorrupted(Vector3Int from);
    public void AddCorruptionSpeed(int corruptionSpeed, Vector3Int from);
}