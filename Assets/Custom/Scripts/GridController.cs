using UnityEngine;
using System.Collections;

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
    public int width = 16;
    public int height = 9;
    public float gridToWorldScale = 1;
    public float platformHeight = 1;

    public Vector3 gridToWorldPosition(Vector2 gridPosition) {
      return new Vector3(
        ((gridPosition.x + gridToWorldScale/2 - ((float)width)/2 + gridPrefab.transform.position.x) * gridToWorldScale),
        (platformHeight/2 * gridToWorldScale),
        ((height - gridPosition.y - gridToWorldScale/2) - ((float)height)/2 + gridPrefab.transform.position.z) * gridToWorldScale);
    }

    // Use this for initialization
    void Start () {
      grid = Instantiate(gridPrefab);
      grid.transform.localScale = new Vector3((float)width, platformHeight, (float)height);
    }

    // Update is called once per frame
    void Update () {

    }
  } 
}
