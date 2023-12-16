using NexTube.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Domain.Entities
{
    public class Report : OwnedEntity
    {
        public enum TypeOfReport {
            sexualContent,
            violentContent,
            abusiveContent,
            harmfulActs,
            spam,
            other
        }

        public ApplicationUser Abuser { get; set; } = null!;

        public VideoEntity? Video { get; set; } = null!;

        public string Body { get; set; } = string.Empty;

        public TypeOfReport Type { get; set; } 

       
    }
}
