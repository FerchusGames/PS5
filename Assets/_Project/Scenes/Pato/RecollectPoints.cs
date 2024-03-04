using UnityEngine;


public class RecollectPoints : MonoBehaviour
{
    [SerializeField] GameObject GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Punto");
        if (other.CompareTag("Cubo"))
        {
            Destroy(other.gameObject);
            GameManager.GetComponent<GameManager>().IncreaseScore(1);
            
            Debug.Log("Punto");
        }
        else if (other.CompareTag("Player"))
        {

        }
    }
}
