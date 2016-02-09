namespace com.terranovita.botretreat
{
    public class Bot : ICreatableFromJson<Bot>
    {

        public string Id { get; set; }

        public string Name { get; set; }

        //public string Color { get; set; }

        public int LocationX { get; set; }

        public int LocationY { get; set; }

        public Orientation Orientation { get; set; }

        public Health PhysicalHealth { get; set; }

        public Health MentalHealth { get; set; }

        public Health Stamina { get; set; }

        public LastAction LastAction { get; set; }

        public Bot FromJson(JSONObject json)
        {
            Id = json.getStringValue("id");
            Name = json.getStringValue("name");
            LocationX = json.getIntValue("locationX");
            LocationY = json.getIntValue("locationY");
            Orientation = json.getEnumValue<Orientation>("orientation");
            PhysicalHealth = json.GetValue<Health>("physicalHealth");
            MentalHealth = json.GetValue<Health>("mentalHealth");
            Stamina = json.GetValue<Health>("stamina");
            LastAction = json.getEnumValue<LastAction>("lastAction");
            return this;
        }
    }
}