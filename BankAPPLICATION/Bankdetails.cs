using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bankapp
{
    using System;
    using System.Collections.Generic;

    namespace BankConsoleProject
    {
        class Program
        {
            static List<User> users = new List<User>();
            static bool isLoggedIn = false;
            static string loggedInUser = "";

            static void Main(string[] args)
            {
                Console.WriteLine("Bank Console Project");

                // Admin Login
                Console.WriteLine("Admin Login");
                Console.Write("Username: ");
                string adminUsername = Console.ReadLine();
                Console.Write("Password: ");
                string adminPassword = Console.ReadLine();
                Console.Write("Role ID (1 for admin): ");
                int adminRoleId = int.Parse(Console.ReadLine());

                if (adminRoleId == 1 && adminUsername == "admin" && adminPassword == "admin123")
                {
                    AdminTasks();
                }
                else
                {
                    Console.WriteLine("Invalid admin credentials. Access denied.");
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }

            static void AdminTasks()
            {
                Console.WriteLine("Welcome, Admin!");
                Console.WriteLine("1. Create User");

                while (true)
                {
                    Console.Write("Enter option (1 to create user, 0 to exit): ");
                    int option = int.Parse(Console.ReadLine());

                    if (option == 1)
                    {
                        CreateUser();
                    }
                    else if (option == 0)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }

                UserLogin();
            }

            static void CreateUser()
            {
                Console.Write("Enter new username: ");
                string username = Console.ReadLine();
                Console.Write("Enter new password: ");
                string password = Console.ReadLine();

                if (!UserExists(username))
                {
                    User newUser = new User(username, password);
                    users.Add(newUser);
                    Console.WriteLine("User created successfully.");
                }
                else
                {
                    Console.WriteLine("Username already exists. Please try again.");
                }
            }

            static bool UserExists(string username)
            {
                foreach (User user in users)
                {
                    if (user.Username == username)
                    {
                        return true;
                    }
                }

                return false;
            }

            static void UserLogin()
            {
                Console.WriteLine("\nUser Login");
                Console.Write("Username: ");
                string username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                Console.Write("Role ID (2 for user): ");
                int roleId = int.Parse(Console.ReadLine());

                if (roleId == 2 && ValidateUser(username, password))
                {
                    isLoggedIn = true;
                    loggedInUser = username;
                    Console.WriteLine($"Welcome, {username}!");
                    UserTasks();
                }
                else
                {
                    Console.WriteLine("Invalid username, password, or role ID. Access denied.");
                }
            }

            static bool ValidateUser(string username, string password)
            {
                foreach (User user in users)
                {
                    if (user.Username == username && user.Password == password)
                    {
                        return true;
                    }
                }

                return false;
            }

            static void UserTasks()
            {
                Console.WriteLine("Welcome, User!");
                Console.WriteLine("1. Add Payee");
                Console.WriteLine("2. Send Money");
                Console.WriteLine("3. Check Account Balance");
                Console.WriteLine("4. Credit Money");

                while (isLoggedIn)
                {
                    Console.Write("Enter option (1 for add payee, 2 for send money, 3 for check balance, 4 for credit money, 0 to logout): ");
                    int option = int.Parse(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            AddPayee();
                            break;
                        case 2:
                            SendMoney();
                            break;
                        case 3:
                            CheckAccountBalance();
                            break;
                        case 4:
                            CreditMoney();
                            break;
                        case 0:
                            Console.WriteLine("Logging out...");
                            isLoggedIn = false;
                            loggedInUser = "";
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }

            static void AddPayee()
            {
                Console.WriteLine("\nAdd Payee");
                Console.Write("Enter payee name: ");
                string payeeName = Console.ReadLine();

                User currentUser = GetUserByUsername(loggedInUser);

                if (currentUser != null && !currentUser.Payees.Contains(payeeName))
                {
                    currentUser.Payees.Add(payeeName);
                    Console.WriteLine("Payee added successfully.");
                }
                else
                {
                    Console.WriteLine("Payee already exists or user not found. Please try again.");
                }
            }

            static void SendMoney()
            {
                Console.WriteLine("\nSend Money");
                Console.Write("Enter payee name: ");
                string payeeName = Console.ReadLine();

                User currentUser = GetUserByUsername(loggedInUser);

                if (currentUser != null && currentUser.Payees.Contains(payeeName))
                {
                    Console.Write("Enter amount to send: ");
                    decimal amount = decimal.Parse(Console.ReadLine());

                    if (amount <= currentUser.Balance)
                    {
                        User payee = GetUserByUsername(payeeName);
                        currentUser.Balance -= amount;
                        payee.Balance += amount;
                        Console.WriteLine($"Sent {amount:C} to {payeeName}.");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient balance.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid payee name or user not found. Please try again.");
                }
            }

            static void CheckAccountBalance()
            {
                Console.WriteLine("\nAccount Balance");
                User currentUser = GetUserByUsername(loggedInUser);

                if (currentUser != null)
                {
                    Console.WriteLine($"Your account balance: {currentUser.Balance:C}");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }

            static void CreditMoney()
            {
                Console.WriteLine("\nCredit Money");
                Console.Write("Enter amount to credit: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                User currentUser = GetUserByUsername(loggedInUser);

                if (currentUser != null)
                {
                    currentUser.Balance += amount;
                    Console.WriteLine($"Amount {amount:C} credited successfully.");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }

            static User GetUserByUsername(string username)
            {
                foreach (User user in users)
                {
                    if (user.Username == username)
                    {
                        return user;
                    }
                }

                return null;
            }
        }

        class User
        {
            public string Username { get; }
            public string Password { get; }
            public decimal Balance { get; set; }
            public List<string> Payees { get; }

            public User(string username, string password)
            {
                Username = username;
                Password = password;
                Balance = 0;
                Payees = new List<string>();
            }
        }
    }

}