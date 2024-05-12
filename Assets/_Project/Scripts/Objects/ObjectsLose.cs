using UnityEngine;

public class ObjectsLose : MonoBehaviour
{

    [SerializeField] private TrayController _trayController;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            _trayController.RemoveFromList(other.GetComponent<Rigidbody>());
            GameManager.Instance.AddFallenFood();
            Destroy(other.gameObject);
        }
    }
}

