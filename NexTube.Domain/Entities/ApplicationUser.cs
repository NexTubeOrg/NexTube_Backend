﻿using Microsoft.AspNetCore.Identity;

namespace NexTube.Domain.Entities {
    public class ApplicationUser : IdentityUser<int> {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Nickname { get; set; }
        public string? Description { get; set; }
        public List<int>? Subscriptions { get; set; } = new List<int>();
        public Guid? ChannelPhotoFileId { get; set; }
    }
}
