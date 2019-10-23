using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    private List<PatrollerController> used = new List<PatrollerController>();
    private List<PatrollerController> free = new List<PatrollerController>();

    public GameObject Produce(int zone)
    {                     
        GameObject patroller_object = null;
        for (int i=0;i<free.Count;i++)
        {
            if (free[i].gameObject.GetComponent<PatrollerController>().zone == zone)
            {
                patroller_object = free[i].gameObject;
                free.Remove(free[i]);
                break;
            }
        }
        if (patroller_object == null)
        {
            patroller_object = Instantiate(Resources.Load<GameObject>("Prefabs/patroller"), new Vector3(0, 0, 0), Quaternion.identity);
            patroller_object.GetComponent<PatrollerController>().zone = zone;
            patroller_object.GetComponent<PatrollerController>().patroller = patroller_object;
            if (zone == 0)
            {
                patroller_object.transform.position = new Vector3(-2.5f, 0.5f, 2.5f);
            }
            else if (zone == 1)
            {
                patroller_object.transform.position = new Vector3(2.5f, 0.5f, 2.5f);
            }
            else
            {
                patroller_object.transform.position = new Vector3(-2.5f, 0.5f, -2.5f);
            }
        }
        used.Add(patroller_object.GetComponent<PatrollerController>());
        return patroller_object;
    }
    
    public void Recover(GameObject patroller_object)
    {
        for(int i = 0;i < used.Count; i++)
        {
            if (patroller_object.GetInstanceID() == used[i].gameObject.GetInstanceID())
            {
                used[i].gameObject.SetActive(false);
                free.Add(used[i]);
                used.Remove(used[i]);
                break;
            }
        }
    }
}
