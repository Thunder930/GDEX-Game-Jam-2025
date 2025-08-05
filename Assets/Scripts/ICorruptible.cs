public interface ICorruptible
{
    public bool IsCorrupted { get; }
    public void AddCorruptionSpeed(int corruptionSpeed);
}