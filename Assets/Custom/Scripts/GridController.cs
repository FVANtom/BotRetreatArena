using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace com.terranovita.botretreat {

  public class GridController : MonoBehaviour {

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

    public GameObject botPrefab;
    public Dictionary<string, BotController> bots;

    private void initialize() {
      if(grid == null) {
        grid = Instantiate(gridPrefab);
      }
      grid.transform.localScale = new Vector3((float)arena.Width, platformHeight, (float)arena.Height);
      grid.GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", new Vector2((float)arena.Width, (float)arena.Height));
      if(bots != null) {
        foreach(var botId in bots.Keys) {
          bots[botId].instantRefresh();
        }
      }
    }

    public Vector3 gridToWorldPosition(int gridPositionX, int gridPositionY) {
      if(arena != null) {
      return new Vector3(
        ((gridPositionX + gridToWorldScale/2 - ((float)arena.Width)/2 + gridPrefab.transform.position.x) * gridToWorldScale),
        (platformHeight/2 * gridToWorldScale),
        ((arena.Height - gridPositionY - gridToWorldScale/2) - ((float)arena.Height)/2 + gridPrefab.transform.position.z) * gridToWorldScale);
      } else {
        return Vector3.zero;
      }
    }

    // Use this for initialization
    void Start () {
      InvokeRepeating("refreshGrid", 0, refreshRate);
    }

    void refreshGrid() {
      Networking.Instance.refreshGrid(successCallback, errorCallback);
    }

    private void successCallback(JSONObject json)
    {
      //Debug.Log(json);
      JSONObject arenaProperties = json.GetField("arena");
      //Debug.Log(arenaProperties);
      if(arenaProperties != null) {
        Arena oldArena = arena;
        arena = Arena.createFrom(arenaProperties);
        if(oldArena == null || (oldArena.Width != arena.Width && oldArena.Height != arena.Height)) {
          initialize();
        }
      }
      JSONObject botsJson = json.GetField("bots");
      //Debug.Log(arenaProperties);
      if(botsJson != null) {
        refreshBots(botsJson);
      }
    }
    private void errorCallback(JSONObject json)
    {
      Debug.Log(json.str);
    }


    private void refreshBots(JSONObject json) {
      Dictionary<string, BotController> newBots = new Dictionary<string, BotController>();
      foreach (JSONObject botJson in json.list)
      {
        Bot bot = Bot.createFrom(botJson);
        BotController currentBot = null;
        if(bots == null || !bots.TryGetValue(bot.Id, out currentBot)) {
          GameObject currentBotGO = Instantiate(botPrefab);
          currentBot = currentBotGO.GetComponent<BotController>();
        }
        currentBot.bot = bot;
        newBots.Add(bot.Id, currentBot);
        //break;
      }
      bots = newBots;
    }


  } 
}
