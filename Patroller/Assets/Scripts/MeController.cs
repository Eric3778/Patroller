using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeController : MonoBehaviour
{
    private bool stop;
    private FirstController action;
    public delegate void Catch_person(string info);
    public static event Catch_person OnCatch;
    public delegate void PublishZone(int zone, Vector3 me_position);
    public static event PublishZone OnZone;
    private GameObject me;
    private int curr_zone;

    // Start is called before the first frame update
    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as FirstController;
        me = action.GetMe();
        stop = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop) return;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 velocity = new Vector3(x, 0f, z);
        Rigidbody playerRigidbody = me.GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        playerRigidbody.MovePosition(me.transform.position + velocity * Time.deltaTime * 2);
        if (x != 0 || z != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(-velocity);
            if (me.transform.rotation != rotation)
                me.transform.rotation = Quaternion.Slerp(me.transform.rotation, rotation, Time.fixedDeltaTime * 5);
        }

        int curr_zone = 0;
        if (me.transform.position.x <= 0f)
        {
            if (me.transform.position.z <= 0f)
            {
                curr_zone = 2;
            }
            else
            {
                curr_zone = 0;
            }
        }
        else
        {
            if (me.transform.position.z >= 0f)
            {
                curr_zone = 1;
            }
            else
            {
                curr_zone = 3;
            }
        }
        OnZone(curr_zone, me.transform.position);
    }

    void Restart()
    {
        stop = false;
    }
    void OnEnable()
    {
        UserGUI.OnRestart += Restart;
    }
    void OnDisable()
    {
        UserGUI.OnRestart -= Restart;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name.Substring(0, 4) == "patr")
            if (OnCatch != null)
            {
                Debug.Log("catch");
                OnCatch("catch me");
                stop = true;
            }
    }
}
