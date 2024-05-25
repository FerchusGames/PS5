using System;
using UnityEngine;

public class DeliverObject : MonoBehaviour
{
    public GameObject _particleSystem;

    private Animator _finishAnimation;
    
    private GameObject _playerObject;
    [SerializeField] private GameObject[] _fireworks;
    private bool _firstTime = true;
    
    private void Start()
    {
        _firstTime = true;
        _playerObject = GameObject.FindWithTag("Player");
        InvokeRepeating(nameof(TryGetFinishAnimation), 0f, 1f);
    }

    private void Update()
    {
        TryGetFinishAnimation();
    }

    private void TryGetFinishAnimation()
    {
        if (!_finishAnimation)
        {
            _finishAnimation = Camera.main.GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (!_finishAnimation)
            {
                _finishAnimation = Camera.main.GetComponent<Animator>();
            }
            
            _finishAnimation.enabled = true;
            
            if (!GameManager.Instance.IsTutorial)
            {
                GameManager.Instance.AddScore(1);
                Destroy(other.gameObject);
                GameObject particleSystem = Instantiate(_particleSystem, other.transform.position, other.transform.rotation);
                particleSystem.transform.SetParent(_playerObject.transform);
                AudioManager.GetInstance().SetAudio(SOUND_TYPE.DELIVER);
            }
            
            if (_firstTime)
            {
                AudioManager.GetInstance().SetAudio(SOUND_TYPE.VICTORY);
                _firstTime = false;
                for (int i = 0; i < _fireworks.Length; i++)
                {
                    _fireworks[i].SetActive(true);
                }
            }
        }
    }
}

