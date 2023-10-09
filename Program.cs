using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

string file = "Tickets.csv";
logger.Info("Program started");

string choice;
// TicketID, Summary, Status, Priority, Submitter, Assigned, Watching
// 1,This is a bug ticket, Open, High, Drew Kjell, Jane Doe, Drew Kjell|John Smith|Bill Jones
do
{
    //prompt user
    Console.WriteLine("1) Display Ticket Info");
    Console.WriteLine("2) Create Ticket");
    Console.WriteLine("Enter any other key to exit.");
    choice = Console.ReadLine();

    logger.Info("User choice: {Choice}", choice);
    // read tickets
    if (choice == "1")
    {
        if (File.Exists(file))
        {
            StreamReader sr = new StreamReader(file);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] arr = line.Split(",");
                // string[] watching = arr[6].Split("|");
                Console.WriteLine("\nTicketID: {0}\nSummary: {1}\nStatus: {2}\nPriority: {3}\nSubmitter: {4}\nAssigned: {5}\nWatching: {6}\n", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6]);
            }
            sr.Close();
        }
        // else
        // {
        //     Console.WriteLine("File does not exist");
        // }
    }
    // write data
    else if (choice == "2")
    {
        StreamReader sr = new StreamReader(file);
        int lines = 1; //generates TicketID

        while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            lines++;
        }

        sr.Close();
        // Get Summary
        Console.WriteLine("Enter Summary for TicketID #{0}: ", lines);
        string summary = Console.ReadLine();
        // Get Status
        Console.WriteLine("Enter Status for TicketID #{0}: ", lines);
        string status = Console.ReadLine();
        // Get Priority
        Console.WriteLine("Enter Priority for TicketID #{0}: ", lines);
        string priority = Console.ReadLine();
        // Submitter
        Console.WriteLine("Enter Submitter for TicketID #{0}: ", lines);
        string submitter = Console.ReadLine();
        //Assigned
        Console.WriteLine("Enter Assigned for TicketID #{0}: ", lines);
        string assigned = Console.ReadLine();
        //  Watching
        Console.WriteLine("Enter Watching for TicketID #{0}: ", lines);
        string watching = Console.ReadLine();

        StreamWriter sw = new StreamWriter(file, append: true);
        sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}", lines, summary, status, priority, submitter, assigned, watching);
        sw.Close();
    }
} while (choice == "1" || choice == "2");

logger.Info("Program ended");