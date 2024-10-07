using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Segue201DemoApp.method
{
    public class AgeProcessor
    {
        public void ValidateAge(int age)
        {
            if (age < 0 || age > 120)
            {
                throw new exception.InvalidAgeException("Invalid Age Count. Age cannot be negative or greater than 120");
            }
            Console.WriteLine($"Age validation successful. {age} is a valid age.");
        }
    }
}
