using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NLog;

public class TicketFile
{
    public string filePath { get; set; }
    public List<Ticket> Tickets { get; set;  }
    private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

    public TicketFile()
    {
        Tickets = new List<Ticket>();
        // Moved logic to separate method
        LoadTickets();        
    }
    private void LoadTickets()
    {
        string[] ticketTypes = { "Bug", "Enhancement", "Task" };
        foreach (string ticketType in ticketTypes)
        {
            string filePath;
            // get proper file
            switch (ticketType)
            {
                case "Bug":
                    filePath = "Tickets.csv";
                    break;
                case "Enhancement":
                    filePath = $"{ticketType}s.csv";
                    break;
                case "Task":
                    filePath = $"{ticketType}.csv";
                    break;
                default:
                    logger.Error($"Invalid ticket type: {ticketType}");
                    continue;
            }

            if (File.Exists(filePath))
            {
                try
                {
                    StreamReader sr = new StreamReader(filePath);
                    sr.ReadLine(); //Skip headers
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        // create a method to read Ticket 
                        Ticket ticket = ReadTicket(line, ticketType); //update new ticketType
                        Tickets.Add(ticket);
                    }
                    sr.Close();
                    logger.Info($"Tickets in file: {Tickets.Count}");

                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
            }
        }
    }

    private Ticket ReadTicket(string line, string ticketType)
    {
        // Modified for different ticket types
        string[] ticketDetails = line.Split(",");
        switch (ticketType)
        {
            case "Bug":
                BugTicket bug = new BugTicket
                {
                    TicketID = int.Parse(ticketDetails[0]),
                    Summary = ticketDetails[1],
                    Status = ticketDetails[2],
                    Priority = ticketDetails[3],
                    Submitter = ticketDetails[4],
                    Assigned = ticketDetails[5],
                    Watching = ticketDetails[6].Split("|").ToList(),
                    Severity = ticketDetails[7]
                };
                return bug;
            case "Enhancement":
                EnhancementTicket enhancement = new EnhancementTicket
                {
                    TicketID = int.Parse(ticketDetails[0]),
                    Summary = ticketDetails[1],
                    Status = ticketDetails[2],
                    Priority = ticketDetails[3],
                    Submitter = ticketDetails[4],
                    Assigned = ticketDetails[5],
                    Watching = ticketDetails[6].Split("|").ToList(),
                    Software = ticketDetails[7],
                    Cost = decimal.Parse(ticketDetails[8]),
                    Reason = ticketDetails[9],
                    Estimate = ticketDetails[10]
                };
                return enhancement;
            case "Task":
                TaskTicket task = new TaskTicket
                {
                    TicketID = int.Parse(ticketDetails[0]),
                    Summary = ticketDetails[1],
                    Status = ticketDetails[2],
                    Priority = ticketDetails[3],
                    Submitter = ticketDetails[4],
                    Assigned = ticketDetails[5],
                    Watching = ticketDetails[6].Split("|").ToList(),
                    ProjectName = ticketDetails[7],
                    DueDate = DateTime.Parse(ticketDetails[8])
                };
                return task;
            default:
                logger.Error($"Failed to read {ticketType} ticket");
                return null;  
        }        
    }
    public void AddTicket(Ticket ticket)
    {
        try
        {
            // Get ticket Type so get proper file
            string ticketType = ticket.GetTicketType();
            string filePath;
            // get proper file
            switch (ticketType)
            {
                case "Bug":
                    filePath = "Tickets.csv";
                    break;
                case "Enhancement":
                    filePath = $"{ticketType}s.csv";
                    break;
                case "Task":
                    filePath = $"{ticketType}.csv";
                    break;
                default:
                    logger.Error($"Invalid ticket type: {ticketType}");
                    return;
            }
            // Generate next ticket ID
            int nextTicketID = Tickets.Max(t => t.TicketID) + 1;
            ticket.TicketID = nextTicketID;
            
            // change to using so don't need close
            using (var sw = new StreamWriter(filePath, true))
            {
                // write to appropriate csv
                sw.WriteLine($"{ticket.TicketID},{ticket.Summary},{ticket.Status},{ticket.Priority},{ticket.Submitter},{ticket.Assigned},{string.Join("|", ticket.Watching)},{GetUniqueTicketData(ticket)}");
            }                        

            Tickets.Add(ticket);
            
            logger.Info($"{ticketType} ticket added. Ticket ID: {ticket.TicketID}");

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    private string GetUniqueTicketData(Ticket ticket)
    {
        // Adds unique ticket data to default information
        if (ticket is BugTicket bug)
            return $"{bug.Severity}";
        else if (ticket is EnhancementTicket enhancement)
            return $"{enhancement.Software},{enhancement.Cost},{enhancement.Reason},{enhancement.Estimate}";
        else if (ticket is TaskTicket task)
            return $"{task.ProjectName},{task.DueDate:MM/dd/yy}";
        else
            return string.Empty;
    }
    public void DisplayTickets()
    {
        foreach (var ticket in Tickets)
        {
            Console.WriteLine(ticket.Display());
        }
    }
}