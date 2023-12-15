using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace FifaApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //VARIABLES
            bool more;
            bool addMember;
            int choice;
            string name, dob, team, position, email, mobile;
            string path = @"D:\Learn\SEM_TWO\OOP\ASSIGNMENT\3\FifaApp\myTeam.json";
            Player player = new Player(null, null, null, null, null);
            DateTime today = DateTime.Now;
            DateTime dateOfBirth;
            Regex nameFormat = new Regex(@"[^a-zA-Z\s]");
            Regex teamFormat = new Regex(@"[^a-zA-Z0-9\s]");
            Regex postionFormat = new Regex("[^a-zA-Z0-9]");
            Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");
            Regex mobileFormat = new Regex("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$");

            //APP START
            do
            {
                Console.Clear();
                //APP HEADER
                Console.WriteLine();
                Console.WriteLine("WELCOME, FIFA TEAM INFO SYSTEM");
                Console.WriteLine();
                Console.WriteLine("Select an option 1...4");
                Console.WriteLine();
                Console.WriteLine("Enter 1 if you want to SHOW ALL.");
                Console.WriteLine("Enter 2 if you want to ADD A NEW MEMBER.");
                Console.WriteLine("Enter 3 if you want to MODIFY INFORMATION.");
                Console.WriteLine("Enter 4 if you want to REMOVE INFORMATION.");
                Console.WriteLine("Enter 5 if you want to DO NOTHING.");
                Console.WriteLine();
                Console.Write("Make your choice : ");
                string recieved = Console.ReadLine();

                //PASSING RIGHT INPUTS
                while (!Int32.TryParse(recieved, out choice) || choice < 1 || choice > 5)
                {
                    Console.Write("Invalid input, Make your choice : ");
                    recieved = Console.ReadLine();
                }

                //CONDITION AS PER THE USER PREFERENCE
                if (choice.Equals(1))
                {
                    Console.Clear();
                    Console.WriteLine("CHOICE : S H O W   A L L");
                    player.ViewTeam(path);
                }
                    
                else if (choice.Equals(2))
                {
                    Console.Clear();
                    Console.WriteLine("CHOISE : N E W    T E A M    M E M B E R");
                    Console.WriteLine();
                    Console.Write("Name :");
                    name = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(name) || nameFormat.IsMatch(name))
                    {
                        Console.Write("Name :");
                        name = Console.ReadLine();
                    }
                    Console.Write("Date of Birth :");
                    dob = Console.ReadLine();
                    while (!DateTime.TryParse(dob, out dateOfBirth))
                    {
                        Console.Write("Date of Birth :");
                        dob = Console.ReadLine();
                    }
                    Console.Write("Team :");
                    team = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(team) || teamFormat.IsMatch(team))
                    {
                        Console.Write("Team :");
                        team = Console.ReadLine();
                    }
                    Console.Write("Position :");
                    position = Console.ReadLine().ToUpper();
                    while (String.IsNullOrWhiteSpace(position) || postionFormat.IsMatch(position))
                    {
                        Console.Write("Position :");
                        position = Console.ReadLine().ToUpper();
                    }
                    Console.Write("Email :");
                    email = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(email) || !emailFormat.IsMatch(email))
                    {
                        Console.Write("Email :");
                        email = Console.ReadLine();
                    }
                    Console.Write("Mobile :");
                    mobile = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(mobile) || !mobileFormat.IsMatch(mobile))
                    {
                        Console.Write("Mobile :");
                        mobile = Console.ReadLine();
                    }
                    player.AddTeam(path, name, dateOfBirth, team, position, email, mobile);
                }
                
                else if (choice.Equals(3))
                {
                    Console.Clear();
                    Console.WriteLine("CHOICE : M O D I F Y    T E A M    M E M B E R");
                    Console.WriteLine();
                    Console.Write("Enter the Name of the Member you looking for : ");
                    name = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(name) || nameFormat.IsMatch(name))
                    {
                        Console.Write("Name :");
                        name = Console.ReadLine();
                    }
                    player.ModifyInfo(path, name);
                }
                    
                else if (choice.Equals(4))
                {
                    Console.Clear();
                    Console.WriteLine("CHOICE : R E M O V E    T E A M    M E M B E R");
                    Console.WriteLine();
                    Console.Write("Enter the Name of the Member you looking for : ");
                    name = Console.ReadLine();
                    while (String.IsNullOrWhiteSpace(name) || nameFormat.IsMatch(name))
                    {
                        Console.Write("Name :");
                        name = Console.ReadLine();
                    }
                    player.RemoveMember(path, name);
                }

                else
                    Console.Write("You choosed want nothing, ");


                Console.Write("Continue (Y/N) : ");
                recieved = Console.ReadLine().ToUpper();
                while (string.IsNullOrWhiteSpace(recieved) || !recieved.Equals("Y") && !recieved.Equals("N"))
                {
                    Console.Write("Invalid input, Continue (Y/N) : ");
                    recieved = Console.ReadLine().ToUpper();
                    
                }
                if(recieved.StartsWith("Y"))
                    more= true;
                else
                    more= false;
            } while (more);


        }
    }
}