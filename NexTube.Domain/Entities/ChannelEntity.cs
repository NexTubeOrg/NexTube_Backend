 
using NexTube.Domain.Entities.Abstract;

namespace NexTube.Domain.Entities
{
    public class ChannelEntity :  OwnedEntity {

       
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Nickname { get; set; }
       

    }
}
