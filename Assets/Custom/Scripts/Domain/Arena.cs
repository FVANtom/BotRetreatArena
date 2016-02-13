using System;

namespace com.terranovita.botretreat
{
    public class Arena : ICreatableFromJson<Arena>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool Private { get; set; }

        public DateTime lastRefreshDateTime { get; set; }

        public Arena FromJson(JSONObject json)
        {
            Id = json.getStringValue("id");
            Name = json.getStringValue("name");
            Active = json.getBoolValue("active", true);
            Width = json.getIntValue("width", 16);
            Height = json.getIntValue("height", 9);
            Private = json.getBoolValue("private");
            string date = json.getStringValue("lastRefreshDateTime");
            lastRefreshDateTime = date != null ? DateTime.Parse(date) : DateTime.Now;
            return this;
        }
    }
}