public class SaveKeys
{
    public class Keys<T>
    {
        public string Key;
        public T defaultValue;

        public Keys(string _key, T _defaultValue)
        {
            Key = _key;
            defaultValue = _defaultValue;
        }
    }
}