using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
    private void OnEnable()
    {
        GameManager.Instance.OnGameReset += Delete;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameReset -= Delete;
    }
    
    private void Delete()
    {
        Destroy(gameObject);
    }
}
