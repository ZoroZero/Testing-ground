using UnityEngine;

public class World : SceneController
{
    // PLayer position
    public Transform player;
    public Vector2 player_Old_Position;


    // Camera
    public GameObject cameraOne;
    public GameObject cameraTwo;

    AudioListener cameraOneAudioLis;
    AudioListener cameraTwoAudioLis;

    public World instance = null;

    public GameObject doorPrefab;
    public GameObject battleCamera;

    // Awake function
    private void Awake()
    {
        cameraOne = GameObject.FindGameObjectWithTag("MainCamera");
        cameraTwo = GameObject.FindGameObjectWithTag("Camera2");
        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();
        cameraTwoAudioLis = cameraTwo.GetComponent<AudioListener>();

        PlayerPrefs.SetInt("CameraPosition", 0);
        //Camera Position Set
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));

        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
    }


    private void OnLevelWasLoaded(int level)
    {
        cameraOne = GameObject.FindGameObjectWithTag("MainCamera");
        //Get Camera Listeners
        cameraOneAudioLis = cameraOne.GetComponent<AudioListener>();

        PlayerPrefs.SetInt("CameraPosition", 0);
        //Camera Position Set
        cameraPositionChange(PlayerPrefs.GetInt("CameraPosition"));
    }


    // Update is called once per frame
    void Update()
    {
        //Change Camera Keyboard
        switchCamera();
    }

    //UI JoyStick Method
    public void cameraPositonM()
    {
        cameraChangeCounter();
    }

    //Change Camera Keyboard
    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            cameraChangeCounter();
        }
    }

    //Camera Counter
    void cameraChangeCounter()
    {
        int cameraPositionCounter = PlayerPrefs.GetInt("CameraPosition");
        cameraPositionCounter++;
        cameraPositionChange(cameraPositionCounter);
    }

    //Camera change Logic
    void cameraPositionChange(int camPosition)
    {
        if (camPosition > 1)
        {
            camPosition = 0;
        }

        //Set camera position database
        PlayerPrefs.SetInt("CameraPosition", camPosition);

        //Set camera position 1
        if (camPosition == 0)
        {
            cameraOne.SetActive(true);
            cameraOneAudioLis.enabled = true;

            cameraTwoAudioLis.enabled = false;
            cameraTwo.SetActive(false);
        }

        //Set camera position 2
        if (camPosition == 1)
        {
            cameraTwo.SetActive(true);
            cameraTwoAudioLis.enabled = true;

            cameraOneAudioLis.enabled = false;
            cameraOne.SetActive(false);
        }

    }

}
