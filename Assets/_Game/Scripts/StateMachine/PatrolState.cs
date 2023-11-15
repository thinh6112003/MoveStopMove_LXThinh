using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IState
{
    public void OnEnter(Bot bot)
    {
        bot.ChangeAnimRun();
    }

    public void OnExecute(Bot bot)
    {
        bot.moveRandom();
    }

    public void OnExit(Bot bot)
    {
    }
}
