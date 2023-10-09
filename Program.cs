using System.ComponentModel;
using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

string file = "Tickets.csv";
logger.Info("Program started");

TicketFile ticketFile = new TicketFile(file);

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
        ticketFile.DisplayTickets();
    }
    // write data
    else if (choice == "2")
    {
        Ticket ticket = new Ticket();
        
        // Get Summary
        Console.Write($"Enter Ticket Summary for #{ticket.TicketID}: ");
        ticket.Summary = Console.ReadLine();
        // Get Status
        Console.WriteLine("Enter Ticket Status for TicketID #{0}: ");
        string status = Console.ReadLine();
        // Get Priority
        Console.WriteLine("Enter Priority for TicketID #{0}: ");
        string priority = Console.ReadLine();
        // Submitter
        Console.WriteLine("Enter Submitter for TicketID #{0}: ");
        string submitter = Console.ReadLine();
        //Assigned
        Console.WriteLine("Enter Assigned for TicketID #{0}: ");
        string assigned = Console.ReadLine();
        //  Watching
        Console.WriteLine("Enter Watching for TicketID #{0}: ");
        string watching = Console.ReadLine();

        // StreamWriter sw = new StreamWriter(file, append: true);
        // sw.WriteLine("{0},{1},{2},{3},{4},{5},{6}", lines, summary, status, priority, submitter, assigned, watching);
        // sw.Close();
    }
} while (choice == "1" || choice == "2");

logger.Info("Program ended");