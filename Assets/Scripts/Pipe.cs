using UnityEngine;

public class Pipe : MonoBehaviour, ICorruptible, IPurifiable
{
    public bool IsCorrupted => throw new System.NotImplementedException();

    public float timeSincePurificationStart { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool purificationStarted { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public int purificationPower { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public void AddCorruptionSpeed(int corruptionSpeed)
    {
        throw new System.NotImplementedException();
    }

    public void SetPurificationPower(int purificationPower)
    {
        throw new System.NotImplementedException();
    }
}
