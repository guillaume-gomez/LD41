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
    public GameObject[] enemys;
    public GameObject player;

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
        Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
    }

    void InitGame()
    {
        InitEnemys();
        InitPlayer();
    }

    void Update()
    {
    }
}