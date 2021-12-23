public interface IPurchase
{
    /// <summary>
    /// Purchase item and deduct money
    /// </summary>
    /// <param name="arg"></param>
    public void Purchase(params object[] arg);
}
