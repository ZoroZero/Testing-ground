using UnityEngine;

public class BattleScene : MonoBehaviour
{
    public GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            changeScene();
            
            Debug.Log(player.transform.position);
            
        }
    }

    private void changeScene()
    {
        //FindObjectOfType<SwitchCamera>().cameraPositonM();
    }
}
