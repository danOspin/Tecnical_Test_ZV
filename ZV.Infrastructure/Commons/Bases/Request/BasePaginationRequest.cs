using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZV.Infrastructure.Commons.Bases.Request
{
    public class BasePaginationRequest
    {
        private int NumPage { get; set; } = 1;
        private int NumRecordsPage { get; set; } = 10;

        private readonly int MaxNumRecordPage = 50;
        private string Order { get; set; } = "asc";

        private string? Sort { get; set; } = null;
        private int Records
        {
            get => NumRecordsPage;
            set
            {
                NumRecordsPage = value > MaxNumRecordPage ? MaxNumRecordPage : value;
            }
        }
    }
}
