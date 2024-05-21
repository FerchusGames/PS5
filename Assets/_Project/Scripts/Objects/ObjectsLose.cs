using UnityEngine;

public class ObjectsLose : MonoBehaviour
{

    [SerializeField] private TrayController _trayController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.AddFallenFood();
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.FALL);
            Destroy(other.gameObject);
        }
    }
}

