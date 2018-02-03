namespace Kuando.Control.Modules.GoogleHangouts.Models
{
    public class Setting
    {
        #region Constructors

        public Setting(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Key { get; }

        public string Value { get; }

        #endregion
    }
}
