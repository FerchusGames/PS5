using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControls : MonoBehaviour
{
    [SerializeField] private Animator _animation;

    private void StopAnimation()
    {
        _animation.speed = 0;
    }

    private void ResumeAnimation()
    {
        _animation.speed = 1;
    }
}
