using UnityEngine;

public class CountdownAudio : MonoBehaviour
{
    public void PlayCountdownBeep()
    {
        AudioManager.GetInstance().SetAudio(SOUND_TYPE.COUNTDOWN_BEEP);
    }

    public void PlayCountdownStart()
    {
        AudioManager.GetInstance().SetAudio(SOUND_TYPE.COUNTDOWN_START);
    }
}
