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
    private GameObject scorePanel;
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
        InitGame();
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
        string[] names = { "Billy", "Willy", "John", "Mike", "Chris", "Paul", "George" };
        for(int i = 0; i < nbEnemys; ++i) {
            float offset = 0.6f;
            Vector3 initPos = new Vector3(InitEnemysPos.x + (i * offset), InitEnemysPos.y, 0);
            EnemyRunner enemy = Instantiate(enemyPrefabs[0], initPos, Quaternion.identity).GetComponent<EnemyRunner>();
            enemy.VelocityX = Random.Range(0.5f, 1.1f);
            enemy.DetectionDistance = Random.Range(30.0f, 50.0f);
            int nameIndex = Random.Range(0, names.Length);
            enemy.Name = names[i];
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
        scorePanel = GameObject.Find("ScorePanel");
        if(counterImage) {
            counterImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);
        }
        if(scorePanel) {
            scorePanel.SetActive(false);
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

    private void EndGame() {
        myTimer.StopTimer();
        player.SetActive(false);
        for(int i = 0; i < enemys.Count; ++i) {
            enemys[i].gameObject.SetActive(false);
        }
        scorePanel.SetActive(true);
        gameOverText = GameObject.Find("ResultMessage").GetComponent<Text>();
        Invoke("ReloadLevel", 5f);
    }

    public void Win() {
        EndGame();
        ScoreManager.instance.CreateScore(true, myTimer.getTimerString(), GetHeroPosition());
        string message = "C o n g r a t s,   y o u   w o n  !";
        gameOverText.text = message;
    }

    public void Lose() {
        EndGame();
        ScoreManager.instance.CreateScore(false, myTimer.getTimerString(), GetHeroPosition());
        string message = "S o r r y,   o n e   o f   y o u r   f r i e n d   t o o k   y o u r   p l a c e   t o   p a r a d i s e.";
        gameOverText.text = message;
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
            return instance.nbEnemys;
        }
    }
}