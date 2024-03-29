using UnityEngine;

public class ObjectsLose : MonoBehaviour
{
    private int objectsFell;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.Save();
            Destroy(other.gameObject);
            objectsFell++;
        }
    }
    
    private void Update()
    {
        if (objectsFell > 2)
        {
            GameManager.Instance.OnLose();
        }
    }
}

