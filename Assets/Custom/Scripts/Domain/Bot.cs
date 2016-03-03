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

            if(json.GetField("stats") != null && json.GetField("stats").GetField("hp") != null) {
                PhysicalHealth = new Health();
                PhysicalHealth.Current = (int)json.GetField("stats").GetField("hp").n;
                PhysicalHealth.Maximum = (int)json.GetField("stats").GetField("maxHp").n;
            }

            if(json.GetField("stats") != null && json.GetField("stats").GetField("energy") != null) {
                Stamina = new Health();
                Stamina.Current = (int)json.GetField("stats").GetField("energy").n;
                Stamina.Maximum = (int)json.GetField("stats").GetField("maxEnergy").n;
            }

            LastAction = json.getEnumValue<LastAction>("lastAction");
            LastAttackLocation = json.GetValue<Position>("lastAttackLocation");
            return this;
        }
    }
}
