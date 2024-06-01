namespace DDOSGuardService.Properties
{
    public class RateLimiterSettings
    {
        public const string Key = "RateLimiter";

        public int MaxRequestsPerTimeFrame { get; set; }
        public double TimeFrameInSeconds { get; set; }
        public string ClientQueryKey { get; set; } = string.Empty;
    }
}