namespace NexTube.Application.CQRS.Identity.Users.Commands.SignInUser {
    public record class UserLookup {
        public int? UserId { get; set; }
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? Email { get; set;}
        public IList<string>? Roles { get; set; }
    }
}
