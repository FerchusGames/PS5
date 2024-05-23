using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextRun : MonoBehaviour
{
    [SerializeField] private MoveAlongPath _moveAlongPath;

    public void StartNextRunEvent()
    {
        _moveAlongPath.StartNextRun();
    }
}
