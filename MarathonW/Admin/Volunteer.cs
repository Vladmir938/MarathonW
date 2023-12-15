using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonW.Admin
{
    public class Volunteer
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public int GenderNumeric { get; set; } // Assuming 0 for female and 1 for male

        public string Gender
        {
            get
            {
                // Convert numeric representation to human-readable format
                return GenderNumeric == 0 ? "Ж" : "М";
            }
        }

        // You may need to add more properties based on your database schema

        // Override ToString method if you want a human-readable representation
        public override string ToString()
        {
            return $"{Name} {Surname} - {Country} ({Gender})";
        }
    }


}
