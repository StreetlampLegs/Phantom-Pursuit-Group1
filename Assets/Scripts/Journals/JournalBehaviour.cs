using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class JournalBehaviour : MonoBehaviour
{
    public Image Journal;

    GameController gc;
    // Start is called before the first frame update
    void Start()
    {
        if (!gc) gc = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void ReadJournal()
    {

    }
}
