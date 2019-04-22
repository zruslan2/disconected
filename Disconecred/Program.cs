using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Disconnected_1
{
    class Program
    {
        static void Main(string[] args)
        {
            AuctionManagement auctionManagement = new AuctionManagement();
            LotItemDto dto = new LotItemDto()
            {
                LotName = "Продажа самолета 'Boeing 767'",
                LotDescription = "Продажа",
                InitialCost = 55000000,
                CreatedByEmployeeId = 1,
                LotId = Guid.NewGuid().ToString(),
                PublishedDate = DateTime.Now
            };

            auctionManagement.OpenLot(dto);
        }
    }
}
