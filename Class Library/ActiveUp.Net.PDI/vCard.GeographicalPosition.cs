namespace ActiveUp.Net.Mail.PDI.vCard
{
    public class GeographicalPosition
    {
        private decimal _longitude, _latitude;

        public decimal Longitude
        {
            get
            {
                return this._longitude;
            }
            set
            {
                this._longitude = value;
            }
        }
        public decimal Latitude
        {
            get
            {
                return this._latitude;
            }
            set
            {
                this._latitude = value;
            }
        }
    }
}
