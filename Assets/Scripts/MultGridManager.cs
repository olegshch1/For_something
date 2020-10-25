using App1;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultGridManager : MonoBehaviour
{

    public int resolution;
    public Sprite sprite;

    public int[,] board;
    public static IGame gameboard = new TwoPlayerGame();

    int Vertical, Horizontal;
    // Start is called before the first frame update
    void Start()
    {
        gameboard = new TwoPlayerGame();
        gameboard.Count = 0;
        Vertical = Horizontal * (Screen.height / Screen.width);
        Horizontal = (int)Camera.main.orthographicSize;
        board = new int[10, 10];
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                board[i, j] = gameboard.Map[i][j];
                Spawn(i, j, board[i, j]);
            }
        }
        //GameObject.Find("MultBoardManager").GetComponent<MultBoardManager>().Block();
    }


    private void Spawn(int x, int y, int val)
    {
        GameObject g = new GameObject("x:" + x + " y:" + y);
        g.transform.position = new Vector3(0.5f * x - 2.2f, 0.5f * y - 2);
        var s = g.AddComponent<SpriteRenderer>();
        s.sprite = sprite;
        switch (val)
        {
            case (1): s.color = Color.red; break;
            case (2): s.color = Color.magenta; break;
            case (3): s.color = Color.green; break;
            case (4): s.color = Color.blue; break;
            case (5): s.color = Color.yellow; break;
            case (6): s.color = Color.black; break;
        }
    }

    void Change(int x, int y, int val)
    {
        switch (val)
        {
            case (1): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.red; break;
            case (2): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.magenta; break;
            case (3): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.green; break;
            case (4): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.blue; break;
            case (5): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.yellow; break;
            case (6): GameObject.Find($"x:{x} y:{y}").GetComponent<SpriteRenderer>().color = Color.black; break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                board[i, j] = gameboard.Map[i][j];
                Change(i, j, board[i, j]);
            }
        }
    }
}
