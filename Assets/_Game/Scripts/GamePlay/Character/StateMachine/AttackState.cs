using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    float timeAttack = 1.5f;
    public void OnEnter(Bot bot)
    {
        bot.isStopMove = true;
        bot.myAgent.SetDestination(bot.transform.position);
        bot.Invoke(nameof(bot.ChangeAnimIdle), 0.5f);
        timer = 0;
        if (bot.botInRange.Count > 0&& bot.botInRange[0]!=null) bot.OnAttack(bot.botInRange[0].transform);
    }

    public void OnExecute(Bot bot)
    {
        timer += Time.deltaTime;
        if (timer > timeAttack) bot.ChangeState(new PatrolState());
    }

    public void OnExit(Bot bot)
    {

    }
}
