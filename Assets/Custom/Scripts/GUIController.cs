using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace com.terranovita.botretreat
{
    public class GUIController : MonoBehaviour
    {

        public Dropdown arenaDropdown;
        public Dropdown creatureDropdown;
        public SmoothFollow smoothFollow;


        // Update is called once per frame
        void Start()
        {
            //Adds a listener to the main slider and invokes a method when the value changes.
            creatureDropdown.onValueChanged.AddListener (delegate {creatureSelected ();});
            arenaDropdown.onValueChanged.AddListener (delegate {arenaSelected ();});
            InvokeRepeating("refreshArenas", 2, 10);
            InvokeRepeating("refreshCreatures", 2, 1);
        }
        public void creatureSelected()
        {
            smoothFollow.target = GridController.Instance.getCreatureByName(creatureDropdown.options[creatureDropdown.value].text);
        }
        public void arenaSelected()
        {
            GridController.Instance.selectArena(arenaDropdown.options[arenaDropdown.value].text);
            creatureDropdown.value = 0;
            #if !UNITY_EDITOR && UNITY_WEBGL
            Application.ExternalCall( "selectArena", arenaDropdown.options[arenaDropdown.value].text );
            #endif
        }

        void refreshArenas() {
            Networking.Instance.refreshArenas(refreshArenasSuccessCallback, errorCallback);
        }

        void refreshCreatures() {
            creatureDropdown.ClearOptions();
            List<string> creatures = GridController.Instance.getCreatureNames();
            creatures.Insert(0,"All");
            creatureDropdown.AddOptions(creatures);
        }

        private void refreshArenasSuccessCallback(JSONObject json)
        {
            if(json.IsArray) {
                List<string> options = new List<string>();
                foreach(JSONObject name in json.list) {
                    options.Add(name.getStringValue("name"));
                }
                arenaDropdown.ClearOptions();
                arenaDropdown.AddOptions(options);
                if(!GridController.Instance.hasSelectedArena()) {
                    arenaSelected();
                }

            }
        }

        private void errorCallback(JSONObject json)
        {
            Debug.Log(json.str);
        }

    }
}
