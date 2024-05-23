using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControls : MonoBehaviour
{
    [SerializeField] private Animator _animation;

    public void StopAnimation()
    {
        _animation.speed = 0;
    }

    public void ResumeAnimation()
    {
        _animation.speed = 1;
    }
}
