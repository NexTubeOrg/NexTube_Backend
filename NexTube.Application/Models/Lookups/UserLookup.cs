﻿namespace NexTube.Application.Models.Lookups
{
    public record class UserLookup
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ChannelPhoto { get; set; }

        public IList<string>? Roles { get; set; }
    }
}
