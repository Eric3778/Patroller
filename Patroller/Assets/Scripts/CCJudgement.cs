using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SituationType : int { Continue, Loss }

public class CCJudgement : MonoBehaviour
{
    public int life;
    public int score;

    void Start ()
    {
        life = 1;
        score = 0;
    }

    public SituationType GetSituation()
    {
        if (life <= 0) return SituationType.Loss;
        return SituationType.Continue;
    }

    public void Record()
    {
        score += 1;
    }

    public void Reset()
    {
        life = 1;
        score = 0;
    }

    public void Catch()
    {
        life -= 1;
    }
}
