using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FifaApp
{
    internal class Player
    {
       

        public Player(string name, DateTime? dateOfBirth, string teamName, string position, List<Contact> contactInformation)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
            TeamName = teamName;
            Position = position;
            ContactInformation = contactInformation;
        }

        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string TeamName { get; set; }
        public string Position { get; set; }
        public List<Contact> ContactInformation { get; set; }

        //INSTENCE METHORDS SECTION

        // 1) VIEW TEAM
        public void ViewTeam(string filePath)
        {
            try
            {
                Console.WriteLine();
                List<Player> players = new List<Player>();
                using (StreamReader sr = new StreamReader(filePath))
                {
                    var jsonString = sr.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("NAME               | DATE OF BIRTH   |   NAME OF THE TEAM   | POSITION | EMAIL                    | MOBILE      ");
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                foreach (var player in players)
                {
                    Console.Write(String.Format("{0,-18} | {1,-15} | {2,-20} | {3,-8} |", player.Name, player.DateOfBirth?.ToString("d"), player.TeamName, player.Position));
                    foreach (var contactPlayer in player.ContactInformation)
                    {
                        Console.WriteLine(String.Format("{0,-25} | {1,-15} ", contactPlayer.Email, contactPlayer.Mobile));
                    }
                }
                Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
                Console.WriteLine();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }            
        }

        // 2) ADD NEW TEAM MEMBER
        public void AddTeam(string filePath, string namePlayer, DateTime birthDay, string nameTeam, string namePosition, string emailPlayer, string mobilePlayer)
        {
            try
            {
                List<Player> players = new List<Player>();
                List<Contact> newContact = new List<Contact>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    var jsonString = sr.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                }

                // Add a new clientto your collection:
                newContact.Add(new Contact(emailPlayer, mobilePlayer));
                players.Add(new Player(namePlayer, birthDay, nameTeam, namePosition, newContact));


                //Serialization the collection information to file:
                using (StreamWriter sr = new StreamWriter(filePath, false))
                {
                    //Serializing bussiness first
                    string jsonData = JsonConvert.SerializeObject(players);
                    sr.Write(jsonData);
                }
                Console.WriteLine("Player added successfully.");
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message); ;
            }
        }

        // 3) MODIFY TEAM MEMBER
        public void ModifyInfo(string filePath, string namePlayer)
        {
            try
            {
                Regex nameFormat = new Regex(@"[^a-zA-Z\s]");
                Regex teamFormat = new Regex(@"[^a-zA-Z0-9\s]");
                Regex postionFormat = new Regex("[^a-zA-Z0-9]");
                Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");
                Regex mobileFormat = new Regex("^(\\+\\d{1,2}\\s)?\\(?\\d{3}\\)?[\\s.-]\\d{3}[\\s.-]\\d{4}$");

                List<Player> players = new List<Player>();
                List<Contact> newContact = new List<Contact>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    var jsonString = sr.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                }

                var chosen = from player in players
                             where player.Name.Contains(namePlayer, StringComparison.OrdinalIgnoreCase)
                             select player;

                int numberOfPlayers = players.Count();
                bool ifChanged = false;
                DateTime dateOfBirth;

                if (chosen.Any())
                {
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        foreach (var contact in players[i].ContactInformation)
                        {
                            if (players[i].Name.Contains(namePlayer, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("found a player name : {0}", players[i].Name);
                                Console.Write("Do you want to modify this (Y/N) : ");
                                string userChoice = Console.ReadLine().ToUpper();
                                while (string.IsNullOrWhiteSpace(userChoice) || !userChoice.Equals("Y") && !userChoice.Equals("N"))
                                {
                                    Console.Write("Invalid input, Continue (Y/N) : ");
                                    userChoice = Console.ReadLine().ToUpper();

                                }
                                if (userChoice.Equals("Y"))
                                {
                                    Console.WriteLine("We go through all the infomation ONE ANT A TIME");
                                    Console.WriteLine("If you do not want to change, press ENTER ");
                                    Console.WriteLine("Current Name is {0}. ", players[i].Name);
                                    Console.Write("Enter the new Name : ");
                                    string newName = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(newName))
                                    {
                                        while (nameFormat.IsMatch(newName) || string.IsNullOrEmpty(newName))
                                        {
                                            Console.Write("Invalid Name format : ");
                                            newName = Console.ReadLine();
                                        }
                                        players[i].Name = newName;
                                        ifChanged = true;
                                    }

                                    Console.WriteLine("Current date of birth : {0} ", players[i].DateOfBirth?.ToString("d"));
                                    Console.Write("Enter the new date of birth : ");
                                    string receive = Console.ReadLine();
                                    if (!String.IsNullOrEmpty(receive))
                                    {
                                        while (!DateTime.TryParse(receive, out dateOfBirth))
                                        {
                                            Console.Write("Enter the new date of birth : ");
                                            receive = Console.ReadLine();
                                        }
                                        players[i].DateOfBirth = dateOfBirth;
                                        ifChanged = true;
                                    }

                                    Console.WriteLine("Current Team is {0}. ", players[i].TeamName);
                                    Console.Write("Enter the new Team : ");
                                    string newTeam = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(newTeam))
                                    {
                                        while (teamFormat.IsMatch(newTeam) || string.IsNullOrEmpty(newTeam))
                                        {
                                            Console.Write("Invalid Team format : ");
                                            newTeam = Console.ReadLine();
                                        }
                                        players[i].TeamName = newTeam;
                                        ifChanged = true;
                                    }

                                    Console.WriteLine("Current Position is {0}. ", players[i].Position);
                                    Console.Write("Enter the new Position : ");
                                    string newPosition = Console.ReadLine().ToUpper();
                                    if (!string.IsNullOrEmpty(newPosition))
                                    {
                                        while (postionFormat.IsMatch(newPosition) || string.IsNullOrEmpty(newPosition))
                                        {
                                            Console.Write("Invalid Position format : ");
                                            newPosition = Console.ReadLine().ToUpper();
                                        }
                                        players[i].Position = newPosition;
                                        ifChanged = true;
                                    }

                                    Console.WriteLine("Current Email is {0}. ", contact.Email);
                                    Console.Write("Enter the new Email : ");
                                    string newEmail = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(newEmail))
                                    {
                                        while (!emailFormat.IsMatch(newEmail) || string.IsNullOrEmpty(newEmail))
                                        {
                                            Console.Write("Invalid Email format : ");
                                            newEmail = Console.ReadLine();
                                        }
                                        contact.Email = newEmail;
                                        ifChanged = true;
                                    }

                                    Console.WriteLine("Current Mobile is {0}. ", contact.Mobile);
                                    Console.Write("Enter the new Mobile : ");
                                    string newMobile = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(newMobile))
                                    {
                                        while (!mobileFormat.IsMatch(newMobile) || string.IsNullOrEmpty(newMobile))
                                        {
                                            Console.Write("Invalid Mobile format : ");
                                            newMobile = Console.ReadLine();
                                        }
                                        contact.Mobile = newMobile;
                                        ifChanged = true;
                                    }
                                }
                            }
                        }
                    }
                    if (!ifChanged)
                    {
                        Console.WriteLine("You didnot make any change");
                    }
                }
                else
                {
                    Console.WriteLine("No such player found on the list");
                }
                if (ifChanged)
                {
                    Console.WriteLine("Need to save information into the file!");
                    using (StreamWriter sr = new StreamWriter(filePath, false))
                    {
                        string jsonString = JsonConvert.SerializeObject(players);
                        sr.Write(jsonString);
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }

        // 4) REMOVE TEAM MEMBER
        public void RemoveMember(string filePath, string namePlayer)
        {
            try
            {
                List<Player> players = new List<Player>();
                List<Contact> newContact = new List<Contact>();

                using (StreamReader sr = new StreamReader(filePath))
                {
                    var jsonString = sr.ReadToEnd();
                    players = JsonConvert.DeserializeObject<List<Player>>(jsonString);
                }

                var chosen = from player in players
                             where player.Name.Contains(namePlayer, StringComparison.OrdinalIgnoreCase)
                             select player;

                int numberOfPlayers = players.Count();
                bool infomationChanged = false;
                if (chosen.Any())
                {
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        foreach (var contact in players[i].ContactInformation)
                        {
                            if (players[i].Name.Contains(namePlayer, StringComparison.OrdinalIgnoreCase))
                            {
                                Console.WriteLine("found a player name : {0}", players[i].Name);
                                Console.Write("Do you want to delete this (Y/N) : ");
                                string userChoice = Console.ReadLine().ToUpper();
                                while (string.IsNullOrWhiteSpace(userChoice) || !userChoice.Equals("Y") && !userChoice.Equals("N"))
                                {
                                    Console.Write("Invalid input, Continue (Y/N) : ");
                                    userChoice = Console.ReadLine().ToUpper();

                                }
                                if (userChoice.Equals("Y"))
                                {
                                    string willBeRemoved = players[i].Name;

                                    bool suceed = players.Remove(players[i]);

                                    if (suceed)
                                    {
                                        Console.WriteLine("Player {0} removed successfully", willBeRemoved);
                                        numberOfPlayers = numberOfPlayers - 1;
                                        i = i - 1;
                                        infomationChanged = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Could not remove client {0}", willBeRemoved);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Player does not exist.");
                }
                if (infomationChanged)
                {
                    using (StreamWriter sr = new StreamWriter(filePath, false))
                    {
                        string jsonData = JsonConvert.SerializeObject(players);
                        sr.Write(jsonData);
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }           
        }
    }
}
