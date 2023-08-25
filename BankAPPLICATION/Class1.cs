using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankProject
{
    public class Program
    {



        static void Main(string[] args)
        {

            User.UserCRUD u = new User.UserCRUD();

            Console.WriteLine("Bank Application");
            Console.WriteLine("Admin Login");
            Console.Write("UserName: ");
            string adminUserName = Console.ReadLine();
            Console.Write("Password: ");
            string adminPassword = Console.ReadLine();
            Console.Write("RoleId 1 for admin :");
            int adminRoleId = Convert.ToInt32(Console.ReadLine());

            if (adminRoleId == 1 && adminUserName == "Admin" && adminPassword == "Admin@123")
            {
                u.AdminTask();

            }
            u.UserTask();
            u.LogOut();
        }

    }
}