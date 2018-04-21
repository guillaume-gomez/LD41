using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int level = 1;
    private int nbEnemys = 5;
    private Timer myTimer;
    public GameObject[] enemys;
    public GameObject player;

    private bool doingSetup;
    private GameObject counterImage;
    private Text counterText;
    public float levelStartDelay = 3f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        InitGame();
    }

    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.level++;
        instance.InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
     static public void CallbackInitialization()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void InitEnemys() {
        for(int i = 0; i < nbEnemys; ++i) {
            float offset = 0.6f;
            Vector3 initPos = new Vector3(0, 2 + (i * offset), 0);
            EnemyRunner enemy = Instantiate(enemys[0], initPos, Quaternion.identity).GetComponent<EnemyRunner>();
            enemy.velocityX = Random.Range(0f, 0.5f);
            //enemy.hasWeapon = true;
        }
    }

    private void InitPlayer() {
        player.SetActive(true);
        //Debug.Log("InitPlayer");
        //Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
    }

    void InitGame()
    {
        doingSetup = true;
        player = GameObject.Find("PlayerWithCamera");
        counterImage = GameObject.Find("CounterImage");
        counterText = GameObject.Find("CounterText").GetComponent<Text>();
        counterText.text = "jsofskdldkfgodsigodsgi " + level;
        counterImage.SetActive(true);
        player.SetActive(false);

        Invoke("HideLevelImage", levelStartDelay);
    }

    private void HideLevelImage()
    {
        counterImage.SetActive(false);
        doingSetup = false;
        //init pos
        StartRun();
    }


    private void StartRun() {
        myTimer = GameObject.Find("MyTimer").GetComponent<Timer>();
        myTimer.StartTimer();

        InitEnemys();
        InitPlayer();
    }

    void Update()
    {
    }

    public void Win() {
        myTimer.StopTimer();
        Debug.Log("You Won");
    }
}