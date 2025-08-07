using UnityEngine;

public interface IPurifiable
{
    public float timeSincePurificationStart { get; set; }
    public bool purificationStarted { get; set; }
    public int purificationPower { get; set; }
    public void SetPurificationPower(int purificationPower, Vector3Int from);
}