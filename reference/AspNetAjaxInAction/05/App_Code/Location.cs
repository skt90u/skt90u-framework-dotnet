using System;

namespace AspNetAjaxInAction
{
    public class Location
    {
        private double latitude = 0.0;
        private double longitude = 0.0;

        public double Latitude
        {
            get { return this.latitude; }
            set { this.latitude = value; }
        }

        public double Longitude
        {
            get { return this.longitude; }
            set { this.longitude = value; }
        }

        public Location()
        { }
    }
}
