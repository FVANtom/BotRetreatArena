using System;

namespace com.terranovita.botretreat
{
    public class Position : ICreatableFromJson<Position>
    {
        public Int32 X { get; set; }

        public Int32 Y { get; set; }

        public Position FromJson(JSONObject json)
        {
            X = json.getIntValue("x");
            Y = json.getIntValue("y");
            return this;
        }
    }
}