using StackExchange.Redis;

namespace AssetManagementApp;

public class MySettings
{
    public string Title { get; set; }

    public string Version { get; set; }

    public StreamInfo Mode { get; set; }

    public string Description { get; set; }
}