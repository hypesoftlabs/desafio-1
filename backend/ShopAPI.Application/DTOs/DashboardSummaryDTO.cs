using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopAPI.Application.DTOs
{
    public class DashboardSummaryDTO
    {
        public long TotalProducts { get; set; }
        public double StorageValueTotal { get; set; }

        public List<ProductDTO> LowStorageProducts { get; set; }
    }
}
