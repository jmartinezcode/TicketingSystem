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
    Console.WriteLine("Press Enter to exit.");
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
        Console.Write("Enter a Quick Summary: ");
        ticket.Summary = Console.ReadLine();
        // Get Status
        Console.Write("Enter Status: ");
        ticket.Status = Console.ReadLine();
        // Get Priority
        Console.Write("Enter Priority: ");
        ticket.Priority = Console.ReadLine();
        // Submitter
        Console.Write("Who is submitting: ");
        ticket.Submitter = Console.ReadLine();
        //Assigned
        Console.Write("Who is assigned: ");
        ticket.Assigned = Console.ReadLine();

        // Watching
        string input;
        do
        {
            Console.Write("Enter Watching (done to quit): ");
            input = Console.ReadLine();
            if (input != "done" && input.Length > 0)
            {
                ticket.Watching.Add(input);
            }
        } while (input != "done");

        // Add ticket to the file
        ticketFile.AddTicket(ticket);
        
    }
} while (choice == "1" || choice == "2");

logger.Info("Program ended");