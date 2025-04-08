using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apr4
{
    public class MyProgram
    {
        public void PerformTask()
        {

            try
            {
                Console.Write("Enter Your Name:");
                string userName = Console.ReadLine();

                Console.Write("Enter Your Last Name:");
                string userLastName = Console.ReadLine();

                Console.Write("Enter Your Age:");
                int userAge = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("User Full Name is: " + userName + " " + userLastName + " And " + "User Age is:" + userAge);

                int votingAge = 18;

                if (userAge >= votingAge)
                    {
                        Console.WriteLine("You Are Eligible For Voting");
                    }

                else
                    {
                        Console.WriteLine("You Are Not Eligible For Voting");
                    }

            }
                catch (Exception ex)
                    {
                        Console.WriteLine("Age is invalid");

                    }

                finally
                    {
                        Console.WriteLine("final");
                    }

        }
    }
}
