using UnityEngine;
using System.Collections;

namespace com.terranovita.botretreat {
    public class TestNetworking : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
          Networking.Instance.refreshGrid("test", successCallback, errorCallback);
        }

        private void successCallback(JSONObject json)
        {
            Debug.Log(json.str);
        }
        private void errorCallback(JSONObject json)
        {
            Debug.Log(json.str);
        }


    }
}