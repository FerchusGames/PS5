using UnityEngine;

public class ObjectsLose : MonoBehaviour
{

    [SerializeField] private TrayController _trayController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.AddFallenFood();
            Destroy(other.gameObject);
        }
    }
}

