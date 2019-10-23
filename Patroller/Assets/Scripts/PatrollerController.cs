using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollerController : MonoBehaviour
{
    public int zone = 0;
    public Vector3 direction;
    public GameObject patroller;
    private Vector3 rand_target;
    private int me_zone;
    private Vector3 me_position;
    private bool stop;

    void Start()
    {
        rand_target = patroller.transform.position;
        stop = false;
    }

    void Update()
    {
        if (stop) return;
        Vector3 curr_target;
        if (me_zone == zone)
            curr_target = me_position;
        else
            curr_target = rand_target;

        if (curr_target != patroller.transform.position)
        {
            patroller.transform.position = Vector3.MoveTowards(patroller.transform.position, curr_target, 1f * Time.deltaTime);
        }
        else
        {
            float ran_x, ran_z;
            if (zone == 0)
            {
                ran_x = Random.Range(-0.2f, -4.8f);
                ran_z = Random.Range(0.2f, 4.8f);
            }
            else if (zone == 1)
            {
                ran_x = Random.Range(0.2f, 4.8f);
                ran_z = Random.Range(0.2f, 4.8f);
            }
            else
            {
                ran_x = Random.Range(-0.2f, -4.8f);
                ran_z = Random.Range(-0.2f, -4.8f);
            }
            rand_target = new Vector3(ran_x, 0.5f, ran_z);
        }
    }

    void End(string info)
    {
        stop = true;
    }

    void Restart()
    {
        stop = false;
    }

    void SetZone(int Me_zone, Vector3 Me_pos)
    {
        me_zone = Me_zone;
        me_position = Me_pos;
    }
    void OnEnable()
    {
        MeController.OnCatch += End;
        MeController.OnZone += SetZone;
        UserGUI.OnRestart += Restart;
    }
    void OnDisable()
    {
        MeController.OnCatch -= End;
        MeController.OnZone -= SetZone;
        UserGUI.OnRestart -= Restart;
    }
}