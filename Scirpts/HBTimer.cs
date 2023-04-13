using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.SceneManagement;

public class HBTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    protected float timer;
    protected float t;
    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();

        t = 20;
    }

    // Update is called once per frame
    void Update()
    {

        t -= Time.deltaTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

        if (t < 0) { SceneManager.LoadScene("EndScene"); }


    }


}
