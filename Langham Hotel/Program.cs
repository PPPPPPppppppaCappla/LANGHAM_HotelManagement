/*
* Project Name:
* Author Name:
* Date:
* Application Purpose:
*
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace Assessment2Task2
{
    // Custom Class - Room
    public class Room
    {
        public int RoomNo { get; set; }
    public bool IsAllocated { get; set; }
    }
    // Custom Class - Customer
    public class Customer
    {
        public int CustomerNo {  get; set; }
    public string CustomerName {  get; set; }
    }
    // Custom Class - RoomAllocation
    public class RoomAllocation
    {
        public int AllocatedRoomNo { get; set; }
    public Customer AllocatedCustomer {  get; set; }
    }
    // Custom Main Class - Program
    class Program
    {
        // Variables declaration and initialization
        public static List<Room> listOfRooms = new List<Room>();
        public static List<RoomAllocation> listOfRoomAllocations = new List<RoomAllocation>();
        public static string filePath;
        // Main function
        static void Main(string[] args)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            filePath = Path.Combine(folderPath, "HotelManagement.txt");
            char ans;
            do
            {
                Console.Clear();
                Console.WriteLine("*****************************************************************");
                Console.WriteLine(" LANGHAM HOTEL MANAGEMENT SYSTEM");
                Console.WriteLine(" MENU");
                Console.WriteLine("***********************************************************************************");
                Console.WriteLine("1. Add Rooms");
                Console.WriteLine("2. Display Rooms");
                Console.WriteLine("3. Allocate Rooms");
                Console.WriteLine("4. De-Allocate Rooms");
                Console.WriteLine("5. Display Room Allocation Details");
                Console.WriteLine("6. Billing");
                Console.WriteLine("7. Save the Room Allocations To a File");
                Console.WriteLine("8. Show the Room Allocations From a File");
                Console.WriteLine("9. Exit");
                Console.WriteLine("0. Backup the Room Allocations");
                Console.WriteLine("***********************************************************************************");
                
                Console.Write("Enter Your Choice Number Here:");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddRooms();// adding Rooms function
                        break;
                    case 2:
                        DisplayRooms();// display Rooms function;
                        break;
                    case 3:
                        AllocateRoom();// allocate Room To Customer function
                        break;
                    case 4:
                        DeAllocateRoom();// De-Allocate Room From Customer function
                        break;
                    case 5:
                        DisplayRoomAllocations();// display Room Alocations function;
                        break;
                    case 6:
                        Console.WriteLine("Billing feature is Under Construction and will be added soon...!!!");// Display "Billing Feature is Under Construction and will
                        break;
                break;
                    case 7:
                        SaveRoomAllocationsToFile();// SaveRoomAllocationsToFile
                        break;
                    case 8:
                        ShowRoomAllocationsFromFile();//Show Room Allocations From File
                        break;
                    case 9:
                        Console.WriteLine("Exiting the application...");// Exit Application
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please try again.");
                        break;
                }
                Console.Write("\nWould You Like To Continue(Y/N):");
                ans = Convert.ToChar(Console.ReadLine());
            } while (ans == 'y' || ans == 'Y');
        }
        public static void AddRooms()
        {
            Console.Write("\nEnter Room Number to Add: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());
            listOfRooms.Add(new Room { RoomNo = roomNo, IsAllocated = false });
            Console.WriteLine($"Room {roomNo} added successfully.");
        }
        public static void DisplayRooms()
        {
            if(listOfRooms.Count == 0)
            {
                Console.WriteLine("No rooms available.");
            }
            else
            {
                Console.WriteLine("Available Rooms: ");
                foreach (var room in listOfRooms)
                {
                    Console.WriteLine($"Room No: {room.RoomNo}, Allocated: {room.IsAllocated}");
                }
            }
        }
        public static void AllocateRoom()
        {
            Console.Write("Enter Room Number to Allocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            Room room = listOfRooms.Find(r => r.RoomNo == roomNo);
            if (room == null || room.IsAllocated)
            {
                Console.WriteLine("Room is either not available or already allocated.");
            }
            Console.Write("Enter Customer Number: ");
            int customerNo = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Customer Name: ");
            string customerName = Console.ReadLine();


            RoomAllocation allocation = new RoomAllocation
            {
                AllocatedRoomNo = roomNo,
                AllocatedCustomer = new Customer { CustomerNo = customerNo, CustomerName = customerName }
            };

            listOfRoomAllocations.Add(allocation);
            room.IsAllocated = true;
            Console.WriteLine($"Room {roomNo} allocated to {customerName}.");
        }

        public static void DeAllocateRoom()
        {
            Console.Write("Enter Room Number to De-Allocate: ");
            int roomNo = Convert.ToInt32(Console.ReadLine());

            RoomAllocation allocation = listOfRoomAllocations.Find(a => a.AllocatedRoomNo == roomNo);
            if (allocation == null)
            {
                Console.WriteLine("Room not found in allocations.");
                return;
            }
            listOfRoomAllocations.Remove(allocation);
            Room room = listOfRooms.Find(r => r.RoomNo == roomNo);
            room.IsAllocated = false;

            Console.WriteLine($"Room {roomNo} de-allocated.");
        }
        public static void DisplayRoomAllocations()
        {
            if (listOfRoomAllocations.Count == 0)
            {
                Console.WriteLine("No room allocations found.");
            }
            else
            {
                Console.WriteLine("Room Allocaations:");
                foreach (var allocation in listOfRoomAllocations)
                {
                    Console.WriteLine($"Room No: {allocation.AllocatedRoomNo}, Customer: {allocation.AllocatedCustomer.CustomerName}");
                }
            }
            
        }
        public static void SaveRoomAllocationsToFile()

        {
            string fileName = "lhms_studentid.txt";
            using (StreamWriter sw = new StreamWriter(filePath, append: true))
            {
                sw.WriteLine($"Room Allocation Details - {DateTime.Now}");
                foreach (var allocation in listOfRoomAllocations)
                {
                    sw.WriteLine($"Room No: {allocation.AllocatedRoomNo}, Customer: {allocation.AllocatedCustomer.CustomerName}");

                }
                sw.WriteLine();
            }
            Console.WriteLine("Room allocations saved to file.");
        }
        public static void ShowRoomAllocationsFromFile()
        {
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string content;
                    while ((content = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(content);
                    }
                }
            }
            else
            {
                Console.WriteLine("No room allocation file found.");
            }
        }
        public static void BackupRoomAllocations()
        {
            string backupFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "HotelManagement_Backup.txt");

            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader (filePath))
                using (StreamWriter sw = new StreamWriter(backupFilePath, append: true))
                {
                    string content;
                    while ((content = sr.ReadLine()) != null)
                    {
                        sw.WriteLine(content);
                    }
                }
                File.WriteAllText(filePath, string.Empty);
                Console.WriteLine("Backup completed and original file cleared.");
            }
            else
            {
                Console.WriteLine("No original room allocation file found to backup.");
            }
        }
    }

}