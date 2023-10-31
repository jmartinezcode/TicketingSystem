using System.ComponentModel;
using NLog;

string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

logger.Info("Program started");

TicketFile ticketFile = new TicketFile();

string choice;

do
{
    //prompt user
    Console.WriteLine("1) Display Ticket Info");
    Console.WriteLine("2) Create Bug Ticket");
    Console.WriteLine("3) Create Enhancement Ticket");
    Console.WriteLine("4) Create Task Ticket");
    Console.WriteLine("5) Search for a Ticket");
    Console.WriteLine("Press Enter to exit.");
    choice = Console.ReadLine();

    logger.Info($"User choice: {choice}");
    // read tickets
    Ticket ticket = null;
    if (choice == "1")
    {
        ticketFile.DisplayTickets();
    }
    // write data for any tickets
    else if (choice == "2" || choice == "3" || choice == "4")
    {             
        // Get Summary
        Console.Write("Enter a Quick Summary: ");
        string summary = Console.ReadLine();
        
        // Get Status
        Console.Write("Enter Status: ");
        string status = Console.ReadLine();
        
        // Get Priority
        Console.Write("Enter Priority: ");
        string priority = Console.ReadLine();
        
        // Submitter
        Console.Write("Who is submitting: ");
        string submitter = Console.ReadLine();
        
        //Assigned
        Console.Write("Who is assigned: ");
        string assigned = Console.ReadLine();

        // Watching 
        List<string> watching = new List<string>();
        string input;
        do
        {
            Console.Write("Enter Watching (done to quit): ");
            input = Console.ReadLine();
            if (input != "done" && input.Length > 0)
            {
                watching.Add(input);
            }
        } while (input != "done");

        // Bug Tickets
        if (choice == "2")
        {
            Console.Write("Enter Severity: ");
            string severity = Console.ReadLine();
            ticket = new BugTicket
            {
                Summary = summary,
                Status = status,
                Priority = priority,
                Submitter = submitter,
                Assigned = assigned,
                Watching = watching,
                Severity = severity
            };            
        }

        // Enhancement Tickets
        else if (choice == "3")
        {
            Console.Write("Enter Software: ");
            string software = Console.ReadLine();
            Console.Write("Enter Cost (in Dollars): ");
            decimal cost = decimal.Parse(Console.ReadLine());
            Console.Write("Enter Reason: ");
            string reason = Console.ReadLine();
            Console.Write("Enter Estimate: ");
            string estimate = Console.ReadLine();
            ticket = new EnhancementTicket
            {
                Summary = summary,
                Status = status,
                Priority = priority,
                Submitter = submitter,
                Assigned = assigned,
                Watching = watching,
                Software = software,
                Cost = cost,
                Reason = reason,
                Estimate = estimate
            };
        }

        // Task Tickets
        else if (choice == "4")
        {            
            Console.Write("Enter Project Name: ");
            string projectName = Console.ReadLine();
            Console.Write("Enter Due Date (MM/DD/YY): ");
            DateTime dueDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yy", null);
            ticket = new TaskTicket 
            {
                Summary = summary,
                Status = status,
                Priority = priority,
                Submitter = submitter,
                Assigned = assigned,
                Watching = watching,
                ProjectName = projectName,
                DueDate = dueDate
            };
        }        

        // Add ticket to the file
        ticketFile.AddTicket(ticket);
        
    }
    // Search tickets
    else if (choice == "5")
    {
        Console.Write("Enter search term (Status, Priority, Submitter): ");
        string userInput = Console.ReadLine();
        string search = userInput.ToLower(); // convert for searching
        Console.ForegroundColor = ConsoleColor.Green;
        var tickets = ticketFile.Tickets.Where(t =>
            t.Status.ToLower().Contains(search) ||
            t.Priority.ToLower().Contains(search) ||
            t.Submitter.ToLower().Contains(search)
        ).ToList();
        Console.WriteLine($"\nThere are {tickets.Count} results for '{userInput}'\n");
        foreach (var t in tickets)
        {
            Console.WriteLine(t.Display());
        }
        Console.ResetColor();
        
    }
    else 
    {
        if (string.IsNullOrEmpty(choice))
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nGoodbye!\n");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\nPlease enter a valid option\n");
        }        
        Console.ResetColor();
    }
} while (!string.IsNullOrEmpty(choice));

logger.Info("Program ended");