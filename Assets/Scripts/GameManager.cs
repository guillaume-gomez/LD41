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
    public GameObject[] enemyPrefabs;
    public Vector3 InitEnemysPos;
    public GameObject player;

    public bool doingSetup;

    private Text gameOverText;
    private Text counterText;
    private GameObject counterImage;
    public float levelStartDelay = 3f;
    private List<EnemyRunner> enemys = new List <EnemyRunner> ();

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
        // uncomment in standalone
        //InitGame();
    }

    static private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
    {
        if(scene.buildIndex != (int)ScreensEnum.GameScreen) {
            return;
        }
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
            Vector3 initPos = new Vector3(InitEnemysPos.x + (i * offset), InitEnemysPos.y, 0);
            EnemyRunner enemy = Instantiate(enemyPrefabs[0], initPos, Quaternion.identity).GetComponent<EnemyRunner>();
            enemy.VelocityX = Random.Range(0f, 1f);
            enemys.Add(enemy);
        }
    }

    private void InitPlayer() {
        //Debug.Log("InitPlayer");
        //Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
    }

    void InitGame()
    {
        enemys.Clear();
        doingSetup = true;
        player = GameObject.Find("Player");
        counterImage = GameObject.Find("CounterImage");
        if(counterImage) {
            counterImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);
        }
        InitEnemys();
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

        InitPlayer();
    }

    void Update()
    {
    }

    void ReloadLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void Win() {
        myTimer.StopTimer();
        player.SetActive(false);
        for(int i = 0; i < enemys.Count; ++i) {
            enemys[i].gameObject.SetActive(false);
        }
        counterImage.SetActive(true);
        counterText = GameObject.Find("CounterText").GetComponent<Text>();
        counterText.enabled = false;
        gameOverText = GameObject.Find("Text").GetComponent<Text>();
        string message = "";
        if(GetHeroPosition() == 1) {
            message = "Congrats, you won !";
        } else {
            message = "Sorry, one of your friend took your place to paradise";
        }
        gameOverText.text = message;
        Invoke("ReloadLevel", 3f);
    }

    public int GetHeroPosition() {
        int position = 1;
        for(int i = 0; i < enemys.Count; i++) {
            if(player.transform.position.x < enemys[i].transform.position.x ) {
                position++;
            }
        }
        return position;
    }

    public int NbEnemys { get
        {
            if(instance == null) {
                instance = this;
            }
            return instance.nbEnemys + 1;
        } }
}