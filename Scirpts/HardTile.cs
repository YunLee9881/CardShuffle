using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HardTile : MonoBehaviour
{
    private bool tileRevealed = false;
    public Sprite originalSprite;
    public Sprite hiddenSprite;


    public void setOriginalSprite(Sprite newSprite)
    {
        originalSprite = newSprite;
    }

    public void hideCard()
    {
        GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        tileRevealed = false;
    }

    public void revealCard()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        tileRevealed = true;
    }

    private void Start()
    {
        hideCard();


    }



    public void OnMouseDown()
    {

        
        GameObject.Find("HardManageCards").GetComponent<HardManager>().cardSelected(gameObject);
    }




}

