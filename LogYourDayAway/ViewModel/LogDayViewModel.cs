using CommunityToolkit.Mvvm.ComponentModel;
using LogYourDayAway.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogYourDayAway.ViewModel
{
    public partial class LogDayViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _selectedRank;

        public List<DayRank> DayRanks { get; } = Enum.GetValues(typeof(DayRank)).Cast<DayRank>().ToList();
    }
}
