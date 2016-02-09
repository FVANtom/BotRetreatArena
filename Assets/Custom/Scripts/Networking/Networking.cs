using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace com.terranovita.botretreat {
    public class Networking : MonoBehaviour
    {

        #region Singleton Code
        /// <summary>
        /// internal instance variable set in the Awake method 
        /// </summary>
        private static Networking _instance = null;

        /// <summary>
        /// The instance of the singleton. Should be only one in a scene. 
        /// </summary>
        public static Networking Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(Networking)) as Networking;

                    if (_instance == null && Application.isEditor)
                        Debug.LogError("Could not locate a Networking object. You have to have exactly one Networking in the scene.");
                }

                return _instance;
            }
        }


        /// <summary>
        /// Sets the instance 
        /// </summary>
        void Awake()
        {
            // See if we are a superfluous instance:
            if (_instance != null)
            {
                Debug.LogError("You can only have one instance of the Networking singleton object in existence.");
            }
            else
                _instance = this;
        }
        #endregion

        private List<WWWMessage> messages = new List<WWWMessage>();
    //public static string ACCESS_POINT_URL = "http://catchthecake.com/botretreat/arena.json?";
    //public static string ACCESS_POINT_URL = "http://botretreat.cloudapp.net/api/game/";
        #if UNITY_EDITOR
        public static string ACCESS_POINT_URL = "http://localhost:8080/api/game/";
        #endif
        #if !UNITY_EDITOR && UNITY_WEBGL
        public static string ACCESS_POINT_URL = "/api/game/";
        #endif


        // register all created WWWMessage objects and keep track of their status and yield progress. If they yield longer than the timeout stop the coroutine and make them call the error callback
        public void addMessage(WWWMessage msg)
        {
          messages.Add(msg);
        }


        public void start()
        {

        }

        public void update() {
          foreach (WWWMessage msg in messages)
          {
            if (msg.hasExpired())
            {
              msg.invokeErrorCallback("Connection timeout", "error");
            }
          }
          messages.RemoveAll(msg => msg.isDone());
        }


        /*
         * This method logs the user in and stored the returned authtoken
         */
        public void refreshGrid(Action<JSONObject> successCallback, Action<JSONObject> errorCallback)
        {
          WWWMessage msg = new WWWMessage(this, "arena/DeathMatchArena", successCallback, errorCallback);
          Networking.Instance.StartCoroutine(msg.send());
        }

    }
}
