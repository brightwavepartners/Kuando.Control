using Busylight;

namespace Kuando.Control.Infrastructure.Models
{
    public class BusyLight
    {
        #region Fields

        private readonly SDK _busyLightInterface;
        private Color _color;

        #endregion

        #region Constructors

        public BusyLight()
        {
            this._busyLightInterface = new SDK();

            this._busyLightInterface.BusyLightChanged += this.BusyLightChanged;
        }

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

        private void BusyLightChanged(object sender, System.EventArgs e)
        {
            this.SetColor();
        }

        private void SetColor()
        {
            switch (this.Color)
            {
                case Color.Red:
                    this._busyLightInterface.Light(BusylightColor.Red);
                    break;
                case Color.Yellow:
                    this._busyLightInterface.Light(BusylightColor.Yellow);
                    break;
                case Color.Green:
                    this._busyLightInterface.Light(BusylightColor.Green);
                    break;
                case Color.Off:
                    this._busyLightInterface.Light(BusylightColor.Off);
                    break;
                default:
                    this._busyLightInterface.Light(BusylightColor.Off);
                    break;
            }
        }

        #endregion
    }
}
