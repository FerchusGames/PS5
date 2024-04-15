using UnityEngine;

public class ObjectsLose : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.AddFallenFood();
            Destroy(other.gameObject);
        }
    }
}

