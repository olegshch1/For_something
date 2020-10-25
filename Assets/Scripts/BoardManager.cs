using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoardManager : MonoBehaviour
{
    public GameObject winText;
    public GameObject lostText;
    int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Sleep()
    {
    //    StartCoroutine(waiter());
    //    IEnumerator waiter()
    //    {
    //        yield return new WaitForSeconds(2);
    //    }
    }

    public void ChangeRedColor()
    {
        GridManager.gameboard.Move(1);
        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ChangeGreenColor()
    {
        GridManager.gameboard.Move(3);

        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ChangeBlueColor()
    {
        GridManager.gameboard.Move(4);

        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ChangeYellowColor()
    {
        GridManager.gameboard.Move(5);

        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ChangeBlackColor()
    {
        GridManager.gameboard.Move(6);

        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ChangePinkColor()
    {
        GridManager.gameboard.Move(2);

        count++;
        if (GridManager.gameboard.Flag)
        {
            winText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
        if (!GridManager.gameboard.Flag && count >= 20)
        {
            lostText.SetActive(true);
            Sleep();
            SceneManager.LoadScene("StartScene");
        }
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
