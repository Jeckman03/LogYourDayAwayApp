namespace LogYourDayAway.Models
{
    public class DayEntryModel : BaseModel
    {
        public DateTime EntryDate { get; set; }
        public string EntryText { get; set; }
        public DayRank DayRank { get; set; } = DayRank.Great;
    }

    public enum DayRank
    {
        Great, Good, Average, Bad, Awful
    }
}
