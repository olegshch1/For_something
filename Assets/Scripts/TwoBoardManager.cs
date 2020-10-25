using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TwoBoardManager : MonoBehaviour
{
    public int count = 0;

    public void Block()
    {
        count = (count + 1) % 2;
        if (count % 2 == 1)
        {
            GameObject.Find("BlueButton (1)").GetComponent<Button>().interactable = true;
            GameObject.Find("RedButton (1)").GetComponent<Button>().interactable = true;
            GameObject.Find("GreenButton (1)").GetComponent<Button>().interactable = true;
            GameObject.Find("BlackButton (1)").GetComponent<Button>().interactable = true;
            GameObject.Find("YellowButton (1)").GetComponent<Button>().interactable = true;
            GameObject.Find("PinkButton (1)").GetComponent<Button>().interactable = true;

            GameObject.Find("BlueButton").GetComponent<Button>().interactable = false;
            GameObject.Find("RedButton").GetComponent<Button>().interactable = false;
            GameObject.Find("GreenButton").GetComponent<Button>().interactable = false;
            GameObject.Find("BlackButton").GetComponent<Button>().interactable = false;
            GameObject.Find("YellowButton").GetComponent<Button>().interactable = false;
            GameObject.Find("PinkButton").GetComponent<Button>().interactable = false;

        }
        else
        {           
            GameObject.Find("BlueButton (1)").GetComponent<Button>().interactable = false;
            GameObject.Find("RedButton (1)").GetComponent<Button>().interactable = false;
            GameObject.Find("GreenButton (1)").GetComponent<Button>().interactable = false;
            GameObject.Find("BlackButton (1)").GetComponent<Button>().interactable = false;
            GameObject.Find("YellowButton (1)").GetComponent<Button>().interactable = false;
            GameObject.Find("PinkButton (1)").GetComponent<Button>().interactable = false;

            GameObject.Find("BlueButton").GetComponent<Button>().interactable = true;
            GameObject.Find("RedButton").GetComponent<Button>().interactable = true;
            GameObject.Find("GreenButton").GetComponent<Button>().interactable = true;
            GameObject.Find("BlackButton").GetComponent<Button>().interactable = true;
            GameObject.Find("YellowButton").GetComponent<Button>().interactable = true;
            GameObject.Find("PinkButton").GetComponent<Button>().interactable = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangeRedColor()
    {
        //if(gameObject.GetComponent<Image>().color == Color.red)
        TwoGridManager.gameboard.Move(1);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
        
    }

    public void ChangeGreenColor()
    {
        TwoGridManager.gameboard.Move(3);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
    }

    public void ChangeBlueColor()
    {
        TwoGridManager.gameboard.Move(4);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
    }

    public void ChangeYellowColor()
    {
        TwoGridManager.gameboard.Move(5);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
    }

    public void ChangeBlackColor()
    {
        TwoGridManager.gameboard.Move(6);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
    }

    public void ChangePinkColor()
    {
        TwoGridManager.gameboard.Move(2);

        
        Block();
        if (TwoGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }
        
    }


    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
