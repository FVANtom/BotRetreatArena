using UnityEngine;
using System.Collections;

namespace com.terranovita.botretreat {
  public class BotController : MonoBehaviour {

    public class OrientationVector {
      public static Vector3 NORTH = new Vector3(0f,0f,1f);
      public static Vector3 EAST = new Vector3(1f,0f,0f);
      public static Vector3 SOUTH = new Vector3(0f,0f,-1f);
      public static Vector3 WEST = new Vector3(-1f,0f,0f);

      public static Vector3 createFrom(Orientation orientation) {
        switch (orientation) {
          case Orientation.North:
            return NORTH;
            break;
          case Orientation.East:
            return EAST;
            break;
          case Orientation.South:
            return SOUTH;
            break;
          case Orientation.West:
            return WEST;
            break;
        }
        return Vector3.zero;
      }
    }
    public enum Orientation {
      North,
      East,
      South,
      West
    }

    private Vector3 targetWorldPosition = Vector3.zero;

    public Vector2 gridPosition = Vector2.zero;
    private Orientation orientation = Orientation.South;
    public Orientation newOrientation = Orientation.South;
    private Vector3 orientationVector = Vector3.zero;
    public float speed = 2;
    public float rotationSpeed = 10;


    void Start() {
      transform.position = GridController.Instance.gridToWorldPosition(gridPosition);
      transform.eulerAngles = OrientationVector.createFrom(orientation);
    }

    void Update() {
      float step = speed * Time.deltaTime;
      targetWorldPosition = GridController.Instance.gridToWorldPosition(gridPosition);
      transform.position = Vector3.MoveTowards(transform.position, targetWorldPosition, step);

      /*
      if(newOrientation != orientation) {
        orientationVector = OrientationVector.createFrom(newOrientation);
        if (Vector3.Distance(transform.eulerAngles, orientationVector) > 0.01f)
        {
          transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, orientationVector, Time.deltaTime*rotationSpeed);
        }
        else
        {
          transform.eulerAngles = orientationVector;
          orientation = newOrientation;
        }
      }*/

      Vector3 targetDir = OrientationVector.createFrom(newOrientation);
      float rotationStep = rotationSpeed * Time.deltaTime;
      Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationStep, 0.0F);
      Debug.DrawRay(transform.position, newDir, Color.red);
      transform.rotation = Quaternion.LookRotation(newDir);

    }


  }
}
