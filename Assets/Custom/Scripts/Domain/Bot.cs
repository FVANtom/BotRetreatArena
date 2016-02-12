using System;

namespace com.terranovita.botretreat
{
    public class Bot : ICreatableFromJson<Bot>
    {

        public String Id { get; set; }

        public String Name { get; set; }

        //public string Color { get; set; }

        public Position Location { get; set; }

        public Orientation Orientation { get; set; }

        public Health PhysicalHealth { get; set; }

        public Health MentalHealth { get; set; }

        public Health Stamina { get; set; }

        public LastAction LastAction { get; set; }

        public Position LastAttackLocation { get; set; }

        public Bot FromJson(JSONObject json)
        {
            Id = json.getStringValue("id");
            Name = json.getStringValue("name");
            Location = json.GetValue<Position>("location");
            Orientation = json.getEnumValue<Orientation>("orientation");
            PhysicalHealth = json.GetValue<Health>("physicalHealth");
            MentalHealth = json.GetValue<Health>("mentalHealth");
            Stamina = json.GetValue<Health>("stamina");
            LastAction = json.getEnumValue<LastAction>("lastAction");
            LastAttackLocation = json.GetValue<Position>("lastAttackLocation");
            return this;
        }
    }
}