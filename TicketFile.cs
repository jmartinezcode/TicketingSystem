using Microsoft.AspNetCore.Mvc;
using NLog;

public class TicketFile
{
    public string filePath { get; set; }
    public List<Ticket> Tickets { get; set;  }
    private static NLog.Logger logger = LogManager.LoadConfiguration(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();

    public TicketFile(string ticketFilePath)
    {
        filePath = ticketFilePath;
        Tickets = new List<Ticket>();

        try
        {
            StreamReader sr = new StreamReader(filePath);
            sr.ReadLine(); //Skip headers
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                // create a method to read Ticket 
                Ticket ticket = ReadTicket(line);
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

    private Ticket ReadTicket(string line)
    {
        string[] ticketDetails = line.Split(",");
        Ticket ticket = new Ticket
        {
            TicketID = int.Parse(ticketDetails[0]),
            Summary = ticketDetails[1],
            Status = ticketDetails[2],
            Priority = ticketDetails[3],
            Submitter = ticketDetails[4],
            Assigned = ticketDetails[5],
            Watching = ticketDetails[6].Split("|").ToList()
        };
        return ticket;
    }
    public void AddTicket(Ticket ticket)
    {
        try
        {
            // Generate next ticket ID
            int nextTicketID = Tickets.Max(t => t.TicketID) + 1;
            ticket.TicketID = nextTicketID;
            StreamWriter sw = new StreamWriter(filePath, true);
            sw.WriteLine($"{ticket.TicketID},{ticket.Summary},{ticket.Status},{ticket.Priority},{ticket.Submitter},{ticket.Assigned},{string.Join("|", ticket.Watching)}");
            sw.Close();

            Tickets.Add(ticket);

            logger.Info("Ticket ID {ID} added", ticket.TicketID);

        }
        catch (Exception ex)
        {
            logger.Error(ex.Message);
        }
    }
    public void DisplayTickets()
    {
        foreach (var ticket in Tickets)
        {
            Console.WriteLine(ticket.ToString());
        }
    }
}