using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultBoardManager : MonoBehaviour
{
    public int count = 0;


    //public void Block()
    //{
    //    count = (count + 1) % 2;
    //    if (count % 2 == 1)
    //    {
    //        GameObject.Find("BlueButton (1)").GetComponent<Button>().interactable = true;
    //        GameObject.Find("RedButton (1)").GetComponent<Button>().interactable = true;
    //        GameObject.Find("GreenButton (1)").GetComponent<Button>().interactable = true;
    //        GameObject.Find("BlackButton (1)").GetComponent<Button>().interactable = true;
    //        GameObject.Find("YellowButton (1)").GetComponent<Button>().interactable = true;
    //        GameObject.Find("PinkButton (1)").GetComponent<Button>().interactable = true;

    //        GameObject.Find("BlueButton").GetComponent<Button>().interactable = false;
    //        GameObject.Find("RedButton").GetComponent<Button>().interactable = false;
    //        GameObject.Find("GreenButton").GetComponent<Button>().interactable = false;
    //        GameObject.Find("BlackButton").GetComponent<Button>().interactable = false;
    //        GameObject.Find("YellowButton").GetComponent<Button>().interactable = false;
    //        GameObject.Find("PinkButton").GetComponent<Button>().interactable = false;

    //    }
    //    else
    //    {
    //        GameObject.Find("BlueButton (1)").GetComponent<Button>().interactable = false;
    //        GameObject.Find("RedButton (1)").GetComponent<Button>().interactable = false;
    //        GameObject.Find("GreenButton (1)").GetComponent<Button>().interactable = false;
    //        GameObject.Find("BlackButton (1)").GetComponent<Button>().interactable = false;
    //        GameObject.Find("YellowButton (1)").GetComponent<Button>().interactable = false;
    //        GameObject.Find("PinkButton (1)").GetComponent<Button>().interactable = false;

    //        GameObject.Find("BlueButton").GetComponent<Button>().interactable = true;
    //        GameObject.Find("RedButton").GetComponent<Button>().interactable = true;
    //        GameObject.Find("GreenButton").GetComponent<Button>().interactable = true;
    //        GameObject.Find("BlackButton").GetComponent<Button>().interactable = true;
    //        GameObject.Find("YellowButton").GetComponent<Button>().interactable = true;
    //        GameObject.Find("PinkButton").GetComponent<Button>().interactable = true;
    //    }
    //}


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
        MultGridManager.gameboard.Move(1);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }


    }

    public void ChangeGreenColor()
    {
        MultGridManager.gameboard.Move(3);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }

    }

    public void ChangeBlueColor()
    {
        MultGridManager.gameboard.Move(4);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }

    }

    public void ChangeYellowColor()
    {
        MultGridManager.gameboard.Move(5);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }

    }

    public void ChangeBlackColor()
    {
        MultGridManager.gameboard.Move(6);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }

    }

    public void ChangePinkColor()
    {
        MultGridManager.gameboard.Move(2);


        //Block();
        if (MultGridManager.gameboard.Flag)
        {
            //winText.SetActive(true);
            //Sleep();
            SceneManager.LoadScene("StartScene");
        }

    }

    public void ToStartScene()
    {
        //GameObject.Find("NetworkManager").GetComponent<NetworkManager>().StopHost();
        SceneManager.LoadScene("StartScene");
    }
}
