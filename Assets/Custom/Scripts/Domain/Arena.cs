using UnityEngine;
using System.Collections;

namespace com.terranovita.botretreat {
  public class Arena
  {
    public string Id { get; set; }

    public string Name { get; set; }

    public bool Active { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public bool Private { get; set; }

    public static Arena createFrom(JSONObject json) {
      Arena a = new Arena();
      a.Id = JsonUtils.getStringValueFrom(json, "id");
      a.Name = JsonUtils.getStringValueFrom(json, "name");
      a.Active = JsonUtils.getBoolValueFrom(json, "active", true);
      a.Width = JsonUtils.getIntValueFrom(json, "width", 16);
      a.Height = JsonUtils.getIntValueFrom(json, "height", 9);
      a.Private = JsonUtils.getBoolValueFrom(json, "private", false);
      return a;
    }

  }

}
