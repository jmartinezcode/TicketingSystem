public abstract class Ticket
{
    public int TicketID { get; set; }
    public string Summary { get; set;  }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Submitter { get; set; }
    public string Assigned { get; set; }
    public List<string> Watching { get; set; }

    public Ticket()
    {
        Watching = new List<string>();
    }
    public virtual string Display()
    {
        return $"TicketID: {TicketID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\n";
    }
    public abstract string GetTicketType();
}

public class BugTicket : Ticket
{
    public string Severity { get; set; }
    public override string Display()
    {
        return base.Display() + $"Severity: {Severity}\n";
    }
    public override string GetTicketType()
    {
        return "Bug";
    }
}

public class EnhancementTicket : Ticket
{
    public string Software { get; set; }
    public decimal Cost { get; set; }
    public string Reason { get; set; }
    public string Estimate { get; set; }
    public override string Display()
    {
        return base.Display() + $"Software: {Software}\nCost: {Cost}\nReason: {Reason}\nEstimate: {Estimate}\n";
    }
    public override string GetTicketType()
    {
        return "Enhancement";
    }
}

public class TaskTicket : Ticket
{
    public string ProjectName { get; set; }
    public DateTime DueDate { get; set; }
    public override string Display()
    {
        return base.Display() + $"Project Name: {ProjectName}\nDue Date: {DueDate:MM/dd/yy}\n";
    }
    public override string GetTicketType()
    {
        return "Task";
    }
}
