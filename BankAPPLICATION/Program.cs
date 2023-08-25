using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{

    public enum Role { Admin = 1, user = 2 }

    public class User
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public double Balance { get; set; }
        public List<string> Payee { get; }

        public Role Role { get; set; }
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            Balance = 0;
            Payee = new List<string>();
        }
        public class UserCRUD
        {
            static bool isLoggedIn = false;
            static string loggedUser = "";
            public List<User> users;

            public UserCRUD()
            {

                users = new List<User>();
            }

            public void AdminTask()
            {
                Console.WriteLine("Welcome Admin");
                while (true)
                {
                    Console.WriteLine("Choose 1 for create user or 0 for Exist");
                    int option = Convert.ToInt32(Console.ReadLine());

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
                        Console.WriteLine("Ivalid option..Please try again");
                    }
                }
                UserLogIn();
            }
            public void CreateUser()
            {
                Console.WriteLine("Enter new UserName: ");
                string userName = Console.ReadLine();
                Console.WriteLine("Enter new Password: ");
                string password = Console.ReadLine();
                if (!UserExists(userName))
                {
                    User newUser = new User(userName, password);
                    users.Add(newUser);
                    Console.WriteLine("User created sucessfully");
                }

            }
            public bool UserExists(string userName)
            {
                foreach (User user in users)
                {
                    if (user.UserName == userName)
                    {
                        return true;
                    }
                }
                return false;
            }
            public void UserLogIn()
            {
                Console.WriteLine("RoleId 2 for User :");
                int RoleId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("User LogIn");
                Console.WriteLine("Enter UserName");
                string username = Console.ReadLine();
                Console.WriteLine("Enter Password");
                string password = Console.ReadLine();
                foreach (User user in users)
                {


                    if (RoleId == 2 && user.UserName == username && user.Password == password)
                    {
                        isLoggedIn = true;
                        loggedUser = username;
                        Console.WriteLine("Validate user");

                    }
                    else
                    {
                        Console.WriteLine("User Already Exist");
                    }


                }
            }

            public void UserTask()
            {
                Console.WriteLine("Welcome, User!");
                Console.WriteLine("1. Credit Amount");
                Console.WriteLine("2. Check Balance");
                Console.WriteLine("3. Add Payee");
                Console.WriteLine("4. Transfer amount");
                Console.WriteLine("5.LogOut");

                while (isLoggedIn)
                {
                    Console.Write("Enter your choice: ");

                    int option = Convert.ToInt32(Console.ReadLine());
                    switch (option)
                    {
                        case 1:
                            CreditAmount();
                            break;
                        case 2:
                            CheckBalance();
                            break;
                        case 3:
                            AddPayee();
                            break;
                        case 4:
                            TransferAmount();
                            break;

                        default:
                            Console.WriteLine("You are logged out ");
                            break;
                    }
                    while (option == 5) ;





                }

            }

            public void CreditAmount()
            {
                Console.WriteLine("Credit Amount");
                Console.WriteLine("Enter amount to credit");
                double amount = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Amount credited sucessfully");
                User currentUser = GetUser(loggedUser);
                if (currentUser != null)
                {
                    currentUser.Balance += amount;
                    Console.WriteLine($"amount {amount} credited sucessfully");

                }

            }

            public User GetUser(string username)
            {
                foreach (User user in users)
                {
                    if (user.UserName == username)
                    {
                        return user;
                    }
                }
                return null;
            }
            public void CheckBalance()
            {
                Console.WriteLine("Your account balance is");
                User currentUser = GetUser(loggedUser);
                if (currentUser != null)
                {

                    Console.WriteLine(currentUser.Balance);

                }

            }



            public void AddPayee()
            {
                Console.WriteLine("Add Payee name");
                string payeeName = Console.ReadLine();
                User currentUser = GetUser(loggedUser);
                if (currentUser != null && !currentUser.Payee.Contains(payeeName))
                {
                    currentUser.Payee.Add(payeeName);
                    Console.WriteLine("Payee Added Sucessfully...");
                }

            }
            public void TransferAmount()
            {
                Console.WriteLine("Enter amount to be send");
                Console.WriteLine("Enter payee name");
                string PayeeName = Console.ReadLine();
                User currentUser = GetUser(loggedUser);
                if (currentUser != null && currentUser.Payee.Contains(PayeeName))
                {
                    Console.WriteLine("Enter amount to send");
                    double amount = Convert.ToDouble(Console.ReadLine());
                    if (amount <= currentUser.Balance)
                    {
                        User payee = GetUser(PayeeName);
                        currentUser.Balance -= amount;
                        payee.Balance += amount;
                        Console.WriteLine($"amount {amount} transfered to {PayeeName}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient Balance");
                    }
                }

            }
            public void LogOut()
            {

            }
        }
    }
}