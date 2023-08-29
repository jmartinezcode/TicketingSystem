using System.Reflection.Metadata.Ecma335;
using Microsoft.Win32.SafeHandles;

string file = "Tickets.csv";
string choice;

// TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
// 1,This is a bug ticket, Open, High, Drew Kjell, Jane Doe, Drew Kjell|John Smith|Bill Jones


do
{
    //prompt user
    Console.WriteLine("1) Read data from file.");
    Console.WriteLine("2) Create file from data.");
    Console.WriteLine("Enter any other key to exit.");
    //input
    choice = Console.ReadLine();

    int count = 0;
    // read data
    if (choice == "1")
    {
        if (File.Exists(file))
        {
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split(",");
                string[] watching = arr[6].Split("|");
                Console.WriteLine("TicketID: {0}, Summary: {1}, Status: {2}, Priority: {3}\nSubmitter: {4}, Assigned: {5}, Watching: {6}", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
                count += 1;
            }
            sr.Close();

        }
        else
        {
            Console.WriteLine("File does not exist");
        }
    }
    // write data
    else if (choice == "2")
    {
        StreamWriter sw = new StreamWriter(file, append: true);
        for (int i = 0; i < 
    }
    

} while (choice == "1" || choice == "2");