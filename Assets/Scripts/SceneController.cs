using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static string prevScene = "";
    public static string currentScene = "";
    public GameObject[] worldMapObject;
    public GameObject[] NPC_MapObject;


    public void initScene(int level)
    {
        Debug.Log("Level: " + level);
        Debug.Log(worldMapObject.Length);
        Instantiate(worldMapObject[level], new Vector3(0, 0, 0), Quaternion.identity);
        // Instantiate(NPC_MapObject[level], new Vector3(0, 0, 0), Quaternion.identity);
    }

}
