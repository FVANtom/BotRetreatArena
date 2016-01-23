using System;

namespace com.terranovita.botretreat {
  public class JsonUtils {

    public static string getStringValueFrom(JSONObject json, string fieldName) {
      JSONObject fieldJson = json.GetField(fieldName);
      if (fieldJson != null && fieldJson.IsString && fieldJson.str != null) {
        return fieldJson.str;
      } else {
        return null;
      }
    }
    public static bool getBoolValueFrom(JSONObject json, string fieldName, bool defaultVal) {
      JSONObject fieldJson = json.GetField(fieldName);
      if (fieldJson != null && fieldJson.IsBool) {
        return fieldJson.b;
      } else {
        return defaultVal;
      }
    }
    public static int getIntValueFrom(JSONObject json, string fieldName, int defaultVal) {
      JSONObject fieldJson = json.GetField(fieldName);
      if (fieldJson != null && fieldJson.IsNumber) {
        return (int)fieldJson.i;
      } else {
        return defaultVal;
      }
    }
  }
}

