using NexTube.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NexTube.Domain.Entities.Report;

namespace NexTube.Application.Models.Lookups
{
    public class ReportLookup
    {
        public UserLookup? Abuser { get; set; } = null!;

        public UserLookup? Creator { get; set; } = null!;

        public string Body { get; set; } = string.Empty;

        public TypeOfReport Type { get; set; }

        public DateTime? DateCreated { get; set; } = null;

        public int? Id { get; set; }

        public int? VideoId { get; set; }           

    }
}
