using System;

namespace com.terranovita.botretreat
{
    public class Health : ICreatableFromJson<Health>
    {
        public Int32 Maximum { get; set; }

        public Int32 Current { get; set; }

        public Int32 Drain { get; set; }

        public Health FromJson(JSONObject json)
        {
            Maximum = json.getIntValue("maximum", 1);
            Current = json.getIntValue("current", 1);
            Drain = json.getIntValue("drain");
            return this;
        }
    }
}