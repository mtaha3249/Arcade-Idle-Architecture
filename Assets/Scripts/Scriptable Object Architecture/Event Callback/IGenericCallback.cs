public interface IGenericCallback
{
    /// <summary>
    /// Handle callback from raised Event
    /// </summary>
    /// <param name="param">parameters to fetch</param>
    public abstract void OnEventRaisedCallback(params object[] param);
}
