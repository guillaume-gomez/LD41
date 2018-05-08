using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject soundManager;
    public GameObject scoreManager;

    void Awake ()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }

        if(ScoreManager.instance == null)
        {
            Instantiate(scoreManager);
        }
    }
}