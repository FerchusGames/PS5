using System;
using UnityEngine;

public class DeliverObject : MonoBehaviour
{
    public GameObject _particleSystem;

    private GameObject _playerObject;
    private bool _firstTime = true;
    
    private void Start()
    {
        _playerObject = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            GameManager.Instance.AddScore(1);
            Destroy(other.gameObject);
            GameObject particleSystem = Instantiate(_particleSystem, other.transform.position, other.transform.rotation);
            particleSystem.transform.SetParent(_playerObject.transform);
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.DELIVER);

            if (_firstTime)
            {
                AudioManager.GetInstance().SetAudio(SOUND_TYPE.VICTORY);
                _firstTime = false;
            }
        }
    }
}

