using System;

namespace FS
{
    class Employee : IInterface
    {
        private event EventHandler _DoTransfer;
        public event EventHandler DoTransfer
        {
            add { _DoTransfer += value; }
            remove { _DoTransfer -= value; }
        }

        private string oldDepartment;
        private string department;
        public string Department
        {
            get
            {
                return department;
            }
            set
            {
                oldDepartment = department;
                department = value;
                _DoTransfer.Invoke(this, null);
            }
        }

        public Employee(string strFirstName, string strLastName, int iAge, string strDepartment)
        {
            FirstName = strFirstName;
            LastName = strLastName;
            Age = iAge;
            department = strDepartment;
            this.DoTransfer += new EventHandler(ShowTransferInfo);
        }

        public virtual void ShowTransferInfo(object sender, EventArgs e)
        {
            Console.WriteLine("{0} {1} from {2} to {3}", FirstName, LastName, oldDepartment, department);
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}
