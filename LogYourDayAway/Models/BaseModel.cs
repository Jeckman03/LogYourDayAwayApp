using CommunityToolkit.Mvvm.ComponentModel;
using LogYourDayAway.Services;
using SQLite;

namespace LogYourDayAway.Models
{
    public abstract class BaseModel : ObservableObject, IEntity
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
    }
}
