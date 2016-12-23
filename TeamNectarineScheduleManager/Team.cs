using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    class Team
    {
        private string teamName;

        public Team()
        {
            this.teamName = "Global team";
        }

        public Team(string teamName)
        {
            this.teamName = teamName;
        }
    }
}
