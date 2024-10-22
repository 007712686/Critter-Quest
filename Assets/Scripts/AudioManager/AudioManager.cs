using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("========= MUSIC ========")]
    [SerializeField] AudioSource musicSource;

    [Header("========= CLIPS ========")]
    public AudioClip musicMenu;
    public AudioClip musicWorld;
    public AudioClip musicBedroom;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //private void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    public void Start()
    {
        assignMusic();
    }

    public void assignMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        switch (sceneName)
        {
            case "MainMenu":
                musicSource.clip = musicMenu;
                break;
            case "critter quest":
                musicSource.clip = musicWorld;
                break;
            case "inside house":
                musicSource.clip = musicBedroom;
                break;
                //add other cases here as scenes come
        }
        musicSource.Play();
    }
}
