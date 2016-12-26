using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamNectarineScheduleManager
{
    class Worker : Employee
    {
        private Team team;
        private ContractType contract;

        public Worker()
        {
            this.team = new Team();
            this.contract = ContractType.FullTime;
        }

        public Worker(Team team, ContractType contract)
        {
            this.team = team;
            this.contract = contract;
        }

    }
}
