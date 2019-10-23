using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActionManager
{
    void Fly(GameObject ufo, float angle, float v);
}

public interface ISceneController
{
    void LoadResources();
}

public interface IUserAction
{
    int GetScore();
    void ReStart();
    int GetLife();
}

public enum SSActionEventType : int { Started, Competeted }


public class SSDirector : System.Object
{
    private static SSDirector _instance;      
    public ISceneController CurrentScenceController { get; set; }
    public static SSDirector GetInstance()
    {
        if (_instance == null)
        {
            _instance = new SSDirector();
        }
        return _instance;
    }
}


public class FirstController : MonoBehaviour, ISceneController, IUserAction
{
    public Factory disk_factory;
    public UserGUI user_gui;
    public CCJudgement judgement;

    private GameObject me;
    private int curr_zone;
    private List<GameObject> patrol_list  = new List<GameObject>(); 
    private bool playing_game = true;

    void Start ()
    {
        SSDirector director = SSDirector.GetInstance();     
        director.CurrentScenceController = this;             
        disk_factory = Singleton<Factory>.Instance;
        judgement = Singleton<CCJudgement>.Instance;
        user_gui = gameObject.AddComponent<UserGUI>() as UserGUI;
        curr_zone = 3;
        this.LoadResources();
    }
	
	void Update ()
    {
    }

    public GameObject GetMe()
    {
        return me;
    }

    void ChangeZone(int Me_zone, Vector3 Me_pos)
    {
        if(Me_zone!=curr_zone && curr_zone!=3)
            judgement.Record();
        curr_zone = Me_zone;
    }

    void End(string info)
    {
        playing_game = false;
        judgement.Catch();
    }

    void OnEnable()
    {
        MeController.OnCatch += End;
        MeController.OnZone += ChangeZone;
    }
    void OnDisable()
    {
        MeController.OnCatch -= End;
        MeController.OnZone -= ChangeZone;
    }

    public void LoadResources()
    {
        me = Instantiate(Resources.Load<GameObject>("Prefabs/me"), new Vector3(2.5f, 0.5f, -2.5f), Quaternion.identity);
        for (int i = 0; i < 3; i++)
        {
            patrol_list.Add(disk_factory.Produce(i));
        }
    }

    public int GetScore()
    {
        return judgement.score;
    }
    public int GetLife()
    {
        return judgement.life;
    }
    public void ReStart()
    {
        playing_game = true;
        judgement.Reset();
        for(int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                patrol_list[i].transform.position = new Vector3(-2.5f, 0.5f, 2.5f);
            }
            else if (i == 1)
            {
                patrol_list[i].transform.position = new Vector3(2.5f, 0.5f, 2.5f);
            }
            else
            {
                patrol_list[i].transform.position = new Vector3(-2.5f, 0.5f, -2.5f);
            }
        }
        me.transform.position = new Vector3(2.5f, 0.5f, -2.5f);
    }
}
