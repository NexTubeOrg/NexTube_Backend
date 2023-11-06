namespace NexTube.Persistence.Identity {
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AuthProviderVerificatorAttribute : Attribute {
        private string? providerName;

        public string? ProviderName { 
            get => providerName; 
            set => providerName = value?.ToLower(); 
        }
    }
}
