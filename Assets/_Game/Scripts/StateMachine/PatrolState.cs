using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    float timer;
    public void OnEnter(Bot bot)
    {
        bot.nextPosition = bot.transform.position;
        bot.isStopMove = false;
        bot.ChangeAnimRun();
        timer = 0;
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        bot.MoveRandom();
        if(timer>5&& bot.botInRange.Count > 0)
        {
            bot.ChangeState(new AttackState());
        }
    }
    
    public void OnExit(Bot bot)
    {
    }
}
