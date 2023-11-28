using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 beginValue;
    void LateUpdate()
    {
        if (LevelManager.Instance.playerGamePlay != null)
        {
            transform.position = LevelManager.Instance.playerGamePlay.transform.position + offset;
        }
        else
        {
            transform.position = beginValue;
        }
    }
}
