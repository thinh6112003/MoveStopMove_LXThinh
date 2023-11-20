using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Bot bot)
    {
        bot.isStopMove = true;
        bot.myAgent.SetDestination(bot.transform.position);
        timer = 0;
        if (bot.botInRange.Count > 0) bot.OnAttack(bot.botInRange[0].transform);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > 2) bot.ChangeState(new PatrolState());
    }

    public void OnExit(Bot bot)
    {

    }
}
