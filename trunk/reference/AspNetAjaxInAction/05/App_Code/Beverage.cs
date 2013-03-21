using System;

namespace AspNetAjaxInAction
{
    public class Beverage
    {
        public Beverage()
        { }

        public Beverage(string name, string desc, double cost)
        {
            this.name = name;
            this.description = desc;
            this.cost = cost;
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        private string description;
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        private double cost;
        public double Cost
        {
            get { return this.cost; }
            set { this.cost = value; }
        }
	
    }
}