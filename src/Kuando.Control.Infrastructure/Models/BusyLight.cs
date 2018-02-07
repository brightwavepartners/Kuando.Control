using Busylight;

namespace Kuando.Control.Infrastructure.Models
{
    public class BusyLight
    {
        #region Fields

        private Color _color;

        #endregion

        #region Properties

        public Color Color
        {
            get => this._color;

            set
            {
                if (value == this.Color)
                {
                    return;
                }

                this._color = value;

                this.SetColor();
            }
        }

        public string Id { get; set; }

        #endregion

        #region Methods

        private void SetColor()
        {
            var sdk = new SDK();

            switch (this.Color)
            {
                case Color.Red:
                    sdk.Light(BusylightColor.Red);
                    break;
                case Color.Yellow:
                    sdk.Light(BusylightColor.Yellow);
                    break;
                case Color.Green:
                    sdk.Light(BusylightColor.Green);
                    break;
                case Color.Off:
                    sdk.Light(BusylightColor.Off);
                    break;
                default:
                    sdk.Light(BusylightColor.Off);
                    break;
            }
        }

        #endregion
    }
}
