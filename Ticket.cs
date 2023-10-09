public class Ticket
{
    public int TicketID { get; set; }
    public string Summary { get; set;  }
    public string Status { get; set; }
    public string Priority { get; set; }
    public string Submitter { get; set; }
    public string Assigned { get; set; }
    public List<string> Watching { get; set; }
    public override string ToString()
    {
        return $"TicketID: {TicketID}\nSummary: {Summary}\nStatus: {Status}\nPriority: {Priority}\nSubmitter: {Submitter}\nAssigned: {Assigned}\nWatching: {string.Join(", ", Watching)}\n";
    }

}