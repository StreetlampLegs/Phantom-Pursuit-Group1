using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JournalBehaviour : MonoBehaviour
{
    public Image Journal;

    bool isPlaying = false;

    GameController gc;
    AudioSource clip;
    // Start is called before the first frame update
    void Start()
    {
        if (!gc) gc = GameObject.Find("GameController").GetComponent<GameController>();
        if (!clip) GetComponent<AudioSource>();
    }

    public void ReadJournal()
    {
        if (isPlaying) return;

        isPlaying = true;

        clip.Play();
    }
}
