using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Security;
using System.Collections.Generic;

namespace com.terranovita.botretreat {
    public class WWWMessage
    {
        private string response = null;
        private string url = null;
        private WWWForm form = null;
        private JSONObject jsonObject = null;
        private bool _done = false;
        private Hashtable urlParams = new Hashtable();
        private Hashtable formParams = new Hashtable();
        private string action;
        protected Action<JSONObject> successCallback = null;
        protected Action<JSONObject> errorCallback = null;
        private WWW www = null;
        private Coroutine coroutine = null;
        private float yieldTime = 0f;
        public float timeout = 30f; // in seconds
        private Networking networking = null;

        public WWW getWWW()
        {
            return www;
        }

        public WWWMessage(Networking networking, string theAction, Action<JSONObject> theSuccessCallback, Action<JSONObject> theErrorCallback)
        {
            action = theAction;
            successCallback = theSuccessCallback;
            errorCallback = theErrorCallback;
            this.networking = networking;
            networking.addMessage(this);
        }

        public void invokeErrorCallback(string message, string status)
        {
            if (!_done)
            {
                this._done = true;
                this.jsonObject = new JSONObject();
                this.jsonObject.AddField("message", message);
                this.jsonObject.AddField("status", status);
                this.response = this.jsonObject.ToString();
                if (errorCallback != null)
                {
                    errorCallback(this.jsonObject);
                }
            }
        }

        public void addUrlParam(string key, string value)
        {
            urlParams.Add(key, value);
        }
        public void addFormParam(string key, string value)
        {
            formParams.Add(key, value);
        }

        private void prepareSend()
        {
            string paramsString = "";
            string hashParams = "";
            string hashForm = "";

            // add date in hash and url as param
            System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
            urlParams.Add("timestamp", Math.Floor(timestamp));

            if (urlParams != null && urlParams.Count > 0)
            {
                foreach (DictionaryEntry entry in urlParams)
                {
                    paramsString += "&" + WWW.EscapeURL("" + entry.Key) + "=" + WWW.EscapeURL("" + entry.Value);
                    hashParams += entry.Value;
                }
            }
            if (formParams != null && formParams.Count > 0)
            {
                form = new WWWForm();
                foreach (DictionaryEntry entry in formParams)
                {
                    form.AddField("" + entry.Key, "" + entry.Value);
                    hashForm += entry.Value;
                }
            }
            this.url = Networking.ACCESS_POINT_URL + action + "/?" + paramsString;
            //Debug.Log(this.url);
        }

        public bool isDone()
        {
            return _done;
        }
        public JSONObject getJsonResponse()
        {
            return jsonObject;
        }
        public string getResponse()
        {
            return response;
        }

        /*
         * Send a request to the server and return the json response
         * 
         */
        public IEnumerator send()
        {
            prepareSend();
            if (form == null)
            {
                www = new WWW(this.url);
            }
            else
            {
                www = new WWW(this.url, form);
            }

            yieldTime = Time.time;
            yield return www;

            //Debug.Log("**************************");
            //foreach( KeyValuePair<string, string> entry in www.responseHeaders) {
            //  Debug.Log(entry.Key+": "+entry.Value);
            //}
            //Debug.Log("**************************");

            if (!_done)
            {
                if (www.error != "" && www.error != null)
                {
                    //Debug.LogError("There was an error getting the data. Action: " + action + ", " + www.error);
                    invokeErrorCallback("Could not connect to the server", "error");
                }
                else
                {
                    this.response = www.text;
                    //Debug.Log(this.response);
                    this.jsonObject = new JSONObject(this.response);
                    successCallback(this.jsonObject);
                }
                _done = true;
            }
        }

        public bool hasExpired()
        {
            return Time.time - yieldTime > timeout;
        }
    }
}
