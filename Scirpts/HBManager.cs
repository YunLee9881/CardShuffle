using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HBManager : MonoBehaviour
{
    public GameObject targetTile;
    int nbMatch;
    int wrongMatch = 0;
    bool timerHasElapsed, timeHasStarted;
    bool _levelHard = false;
    float timer;
    int cardShape;
    private string rowForCard1, rowForCard2;
    public GameObject card;
    private bool firstCardSelected, secondCardSelected;
    private GameObject card1, card2;
    string CardShape;
    public KeyCode code;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClip2;
    public AudioClip ClickAudioClip;
    public AudioClip ClickAudioClip2;
    public AudioClip WrongAudioClip;
    public TextMeshProUGUI levelText;
    public GameObject MainCanvas;


    public void OnClickEndButton(string SceneName)
    {
        SceneManager.LoadScene("EndScene");
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

    }

    public int[] createShuffledArray()
    {
        int[] newArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        int tmp;
        for (int t = 0; t < 10; t++)
        {
            tmp = newArray[t];
            int r = Random.Range(t, 10);
            newArray[t] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }
    public int CardShapeClip()
    {
        cardShape = Random.Range(2, 4);
        return cardShape;
    }
    public void cardShapeSelected()
    {

        if (cardShape == 2) { CardShape = "_of_hearts"; }
        else if (cardShape == 3) { CardShape = "_of_diamonds"; }

    }


    void addACard(int row, int rank, int value)
    {

        float cardOriginalScale = card.transform.localScale.x;
        float scaleFactor = (500 * cardOriginalScale) / 100.0f;
        GameObject cen = GameObject.Find("centerOfScreen");

        //Vector3 newPosition = new Vector3(cen.transform.position.x + ((rank - 10 / 2) * scaleFactor), cen.transform.position.y, cen.transform.position.z);
        float yScaleFactor = (725 * cardOriginalScale) / 100.0f;
        Vector3 newPosition = new Vector3(cen.transform.position.x + ((rank - 10 / 2) * scaleFactor), cen.transform.position.y + ((row - 2 / 2) * yScaleFactor), cen.transform.position.z);
        GameObject C = (GameObject)(Instantiate(card, newPosition, Quaternion.identity));
        C.tag = "" + (value + 1);
        C.name = "" + row + "_" + value;

        string nameOfCard = "";
        string cardNumber = "";
        if (value == 0)
        {
            cardNumber = "ace";
        }
        else
        {
            cardNumber = "" + (value + 1);
        }
        cardShapeSelected();
        nameOfCard = cardNumber + CardShape;
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nameOfCard));
        GameObject.Find("" + row + "_" + value).GetComponent<HBTile>().setOriginalSprite(s1);
    }



    private void displayCards()
    {
        //Instantiate(card, new Vector3(0,0,0), Quaternion.identity);
        //addACard(0);
        int[] shuffledArray = createShuffledArray();
        int[] shuffledArray2 = createShuffledArray();
        for (int i = 0; i < 10; i++)
        {
            //Instantiate(card, new Vector3(0,0,0), Quaternion.identity);
            //addACard(i,shuffledArray[i]);
            addACard(0, i, shuffledArray[i]);
            addACard(1, i, shuffledArray2[i]);

        }

    }
    public void lagh()
    {
        audioSource.PlayOneShot(audioClip2);
    }

    //start is called before the first frame update
    void Start()
    {
        lagh();
        CardShapeClip();
        displayCards();
        targetTile.gameObject.SetActive(false);
        Destroy(targetTile);

    }

    public void runTimer()
    {
        timerHasElapsed = false;
        timeHasStarted = true;
    }
    public void checkCards()
    {
        runTimer();
    }
    
    public void resetKey()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("HardScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        resetKey();
        if (timeHasStarted)
        {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 0.5)
            {
                timerHasElapsed = true;
                timeHasStarted = false;

                if (card1.tag == card2.tag)
                {
                    Destroy(card1);
                    Destroy(card2);
                    audioSource.gameObject.SetActive(true);
                    audioSource.PlayOneShot(audioClip);
                    nbMatch++;
                    wrongMatch = 0;
                    if (nbMatch == 10)
                    {
                        WaitAndDoSomething();
                        SceneManager.LoadScene("SuccScene");
                    }
                }
                else
                {
                    wrongMatch++;
                    if (wrongMatch == 3) { SceneManager.LoadScene("EndScene"); }
                    audioSource.PlayOneShot(WrongAudioClip);
                    card1.GetComponent<HBTile>().hideCard();
                    card2.GetComponent<HBTile>().hideCard();
                }


                firstCardSelected = false;
                secondCardSelected = false;
                card1 = null;
                card2 = null;
                rowForCard1 = "";
                rowForCard2 = "";
                timer = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }


    public void cardSelected(GameObject card)
    {

        if (!firstCardSelected)
        {
            string row = card.name.Substring(0, 1);
            rowForCard1 = row;
            firstCardSelected = true;
            card1 = card;
            card1.GetComponent<HBTile>().revealCard();
            audioSource.PlayOneShot(ClickAudioClip);
        }
        else if (firstCardSelected && !secondCardSelected)
        {
            string row = card.name.Substring(0, 1);
            rowForCard2 = row;

            if (rowForCard2 != rowForCard1)
            {
                audioSource.PlayOneShot(ClickAudioClip);
                secondCardSelected = true;
                card2 = card;
                card2.GetComponent<HBTile>().revealCard();
                checkCards();
            }
        }
    }


    public void OnClickCard()
    {
        audioSource.PlayOneShot(audioClip);
    }

    IEnumerator WaitAndDoSomething()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        // 2초 후에 실행할 코드
    }


}
