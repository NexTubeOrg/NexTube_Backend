using NexTube.Domain.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexTube.Domain.Entities
{
    public class VideoAccessModificatorEntity : BaseEntity
    {
        public string Modificator { get; set; } = string.Empty;
    }
}
