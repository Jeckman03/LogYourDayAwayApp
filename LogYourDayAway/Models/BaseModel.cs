using SQLite;

namespace LogYourDayAway.Models
{
    public abstract class BaseModel
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }
}
