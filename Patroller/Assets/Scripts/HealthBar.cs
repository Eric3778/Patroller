using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    private FirstController SceneController;
    public Slider mySlider;
    // Start is called before the first frame update
    void Start()
    {
        SceneController = SSDirector.GetInstance().CurrentScenceController as FirstController;
    }

    // Update is called once per frame
    void Update()
    {
        int life = SceneController.GetLife();
        if (life == 1)
        {
            mySlider.value = 100;
        }
        else
        {
            mySlider.value = 0;
        }
        this.transform.LookAt(this.transform.position - new Vector3(0f, -2f, 1f));
    }
}
