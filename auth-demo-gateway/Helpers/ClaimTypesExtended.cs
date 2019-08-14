namespace auth_demo_gateway.Helpers
{
    public static class ClaimTypesExtended
    {
        /// <summary>
        /// Returns the 'oid' value from a JWT. This holds the AD Unique ID for the user
        /// </summary>
        public const string ObjectIdentifier = "http://schemas.microsoft.com/identity/claims/objectidentifier";
    }
}
