using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimation : MonoBehaviour
{
    public Animation _Animation;

public void PlayFadeOut()
    {
        _Animation.Play("FadeOut");
    }

}