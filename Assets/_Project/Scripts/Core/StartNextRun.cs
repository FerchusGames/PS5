using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNextRun : MonoBehaviour
{
    [SerializeField] private MoveAlongPath _moveAlongPath;
    [SerializeField] private GameObject _enableObject;
    [SerializeField] private Animator _animator;
    
    public void StartNextRunEvent()
    {
        if (GameManager.Instance.IsTutorial)
        {
            _animator.speed = 0;
            _enableObject.SetActive(true);
        }
        else
        {
            _moveAlongPath.StartNextRun();
        }
    }
}
