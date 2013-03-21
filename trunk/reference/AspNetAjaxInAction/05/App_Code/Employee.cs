using System;

namespace AspNetAjaxInAction
{
    public class Employee
    {
        public Employee()
        {

        }

        private string first;
        public string First
        {
            get { return this.first; }
            set { this.first = value; }
        }

        private string last;
        public string Last
        {
            get { return this.last; }
            set { this.last = value; }
        }

        private string title;
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }
	
    }
}
