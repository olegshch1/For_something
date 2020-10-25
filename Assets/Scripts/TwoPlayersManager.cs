using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TwoPlayersManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ToTwoBoardScene()
    {
        SceneManager.LoadScene("TwoGameScene");
    }

    public void ToMultScene()
    {
        SceneManager.LoadScene("MultScene");
    }
}
