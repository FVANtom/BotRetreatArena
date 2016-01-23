using UnityEngine;
using System.Collections;

namespace com.terranovita.botretreat {
  public class Bot {

      public string Id { get; set; }

      public string Name { get; set; }
    /*
      public string Color { get; set; }
*/
      public int LocationX { get; set; }

      public int LocationY { get; set; }

      public Orientation Orientation { get; set; }
      /*
      public Health PhysicalHealth { get; set; }

      public Health MentalHealth { get; set; }

      public Health Stamina { get; set; }
      */


      public static Bot createFrom(JSONObject json) {
        Bot a = new Bot();
        a.Id = JsonUtils.getStringValueFrom(json, "id");
        a.Name = JsonUtils.getStringValueFrom(json, "name");
        a.LocationX = JsonUtils.getIntValueFrom(json, "locationX", 0);
        a.LocationY = JsonUtils.getIntValueFrom(json, "locationY", 0);
        a.Orientation = (Orientation)JsonUtils.getIntValueFrom(json, "orientation", 0);
        return a;
      }
  }
}