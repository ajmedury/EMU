﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emu.Common.DataContracts;

namespace Emu.Common
{
    public class Equipment
    {
        public int BarCode { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime WarrantyExpiration { get; set; }

        public List<Software> InstalledSoftware { get; set; }
        public List<Ticket> MaintenanceTickets { get; set; }
        public List<NetworkAddress> NetworkAddresses { get; set; }

        public Equipment()
        {
            InstalledSoftware = new List<Software>();
            MaintenanceTickets = new List<Ticket>();
            NetworkAddresses = new List<NetworkAddress>();
        }
    }
}
