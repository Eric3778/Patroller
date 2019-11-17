using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    private IUserAction action;
    public delegate void Restart();
    public static event Restart OnRestart;

    void Start ()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    }
	
	void OnGUI ()
    {
        GUISkin skin = GUI.skin;
        skin.button.normal.textColor = Color.black;
        skin.horizontalScrollbar.normal.textColor = Color.red;
        skin.label.normal.textColor = Color.black;
        skin.button.fontSize = 20;
        skin.label.fontSize = 20;
        GUI.skin = skin;
        int life = action.GetLife();

        if(life > 0)
        {
            //GUI.HorizontalScrollbar(new Rect(0, 5, Screen.width / 8, Screen.height / 32), 1.0f, 1.0f, 0.0f, 1.0f);
            GUI.Label(new Rect(0, Screen.height / 16, Screen.width / 8, Screen.height / 16), "Score:" + action.GetScore().ToString());
        }
        else
        {
            //GUI.HorizontalScrollbar(new Rect(0, 5, Screen.width / 8, Screen.height / 32), 0.0f, 1.0f, 0.0f, 1.0f);
            GUI.Label(new Rect(Screen.width/2-60, Screen.height*5/16, 120, Screen.height / 8), "Game Over!");
            GUI.Label(new Rect(Screen.width/2-75, Screen.height*7/16, 200, Screen.height / 8), "Your score is:"+ action.GetScore().ToString());
            if (GUI.Button(new Rect(Screen.width * 3 / 8, Screen.height * 9 / 16, Screen.width / 4, Screen.height / 8), "Restart"))
            {
                OnRestart();
                action.ReStart();
                return;
            }
        }
    }
}
