using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace com.terranovita.botretreat
{

    public class GridController : MonoBehaviour
    {

        #region Singleton Code
        /// <summary>
        /// internal instance variable set in the Awake method 
        /// </summary>
        private static GridController _instance = null;


        /// <summary>
        /// The instance of the singleton. Should be only one in a scene. 
        /// </summary>
        public static GridController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType(typeof(GridController)) as GridController;

                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        _instance = go.AddComponent<GridController>();
                    }
                }

                return _instance;
            }
        }
        public static bool exists()
        {
            return _instance != null;
        }

        #endregion

        public GameObject gridPrefab;
        public GameObject grid;
        public float gridToWorldScale = 1;
        public float platformHeight = 1;
        public float refreshRate = 1;
        private Arena arena;
        private string selectedArena;

        public GameObject botPrefab;
        public GameObject nameTagPrefab;
        public GameObject healthTagPrefab;
        public GameObject staminaTagPrefab;
        public Dictionary<String, BotController> _bots = new Dictionary<string, BotController>();

        private void initialize()
        {
            if (grid == null)
            {
                grid = Instantiate(gridPrefab);
            }
            grid.transform.localScale = new Vector3((float)arena.Width, platformHeight, (float)arena.Height);
            grid.GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", new Vector2((float)arena.Width, (float)arena.Height));
            if (_bots != null)
            {
                foreach (var botId in _bots.Keys)
                {
                    _bots[botId].instantRefresh();
                }
            }
        }

        public Vector3 gridToWorldPosition(int gridPositionX, int gridPositionY)
        {
            if (arena != null)
            {
                return new Vector3(
                  ((gridPositionX + gridToWorldScale / 2 - ((float)arena.Width) / 2 + gridPrefab.transform.position.x) * gridToWorldScale),
                  (platformHeight / 2 * gridToWorldScale),
                  ((arena.Height - gridPositionY - gridToWorldScale / 2) - ((float)arena.Height) / 2 + gridPrefab.transform.position.z) * gridToWorldScale);
            }
            else {
                return Vector3.zero;
            }
        }

        // Use this for initialization
        void Start()
        {
            InvokeRepeating("refreshGrid", 0, refreshRate);
        }

        public void selectArena(string name) {
            this.selectedArena = name;
            refreshGrid();
        }

        void refreshGrid()
        {
            if(hasSelectedArena()) {
                Networking.Instance.refreshGrid(this.selectedArena, successCallback, errorCallback);
            }
        }

        public bool hasSelectedArena() {
            return this.selectedArena != null;
        }

        private void successCallback(JSONObject json)
        {
            var oldArena = arena;
            arena = json.GetValue<Arena>("arena");
            if (oldArena == null || (oldArena.Width != arena.Width && oldArena.Height != arena.Height))
            {
                initialize();
            }
            var bots = json.GetValues<Bot>("bots");
            refreshBots(bots);
        }

        private void errorCallback(JSONObject json)
        {
            Debug.Log(json.str);
        }


        private void refreshBots(List<Bot> bots)
        {
            CleanKilledBots(bots);
            foreach (var bot in bots)
            {
                BotController currentBotController = null;
                _bots.TryGetValue(bot.Id, out currentBotController);
                if (currentBotController == null)
                {
                    var currentBotGO = Instantiate(botPrefab);
                    currentBotController = currentBotGO.GetComponent<BotController>();

                    var currentNameTagGO = Instantiate(nameTagPrefab);
                    currentNameTagGO.transform.parent = currentBotController.head;
                    var nameTagController = currentNameTagGO.GetComponent<NameTagController>();
                    nameTagController.BotGameObject = currentBotGO;
                    currentBotController.NameTagController = nameTagController;

                    var currentHealthTagGO = Instantiate(healthTagPrefab);
                    var healthController = currentHealthTagGO.GetComponent<HealthTagController>();
                    currentHealthTagGO.transform.parent = currentBotController.head;
                    healthController.BotGameObject = currentBotGO;
                    currentBotController.HealthController = healthController;
          /*
                    var currentStaminaTagGO = Instantiate(staminaTagPrefab);
                    var staminaController = currentStaminaTagGO.GetComponent<StaminaTagController>();
                    currentStaminaTagGO.transform.parent = currentBotController.head;
                    staminaController.BotGameObject = currentBotGO;
                    currentBotController.StaminaController = staminaController;*/

                    _bots.Add(bot.Id, currentBotController);
                }
                currentBotController.UpdateBot(bot);
            }
        }

        private void CleanKilledBots(List<Bot> currentBots)
        {
            var cachedBotIds = _bots.Select(x => x.Key).ToList();
            var currentBotIds = currentBots.Select(x => x.Id).ToList();
            foreach (var cachedBotId in cachedBotIds)
            {
                if (!currentBotIds.Contains(cachedBotId))
                {
                    _bots[cachedBotId].Destroy();
                    _bots.Remove(cachedBotId);
                }
            }
        }

        public List<string> getCreatureNames() {
            List<string> toRet = new List<string>();
            foreach (var botId in _bots.Keys)
            {
                toRet.Add(botId);
            }
            return toRet;
        }

        public Transform getCreatureById(string botId) {
            Transform toRet = null;
            BotController controller = null;
            _bots.TryGetValue(botId, out controller);
            if(controller != null) {
                toRet = controller.transform;
            }
            return toRet;
        } 
    }
}