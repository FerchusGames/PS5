using System;
using UnityEngine;

public class DeliverObject : MonoBehaviour
{
    public GameObject _particleSystem;

    private Animator _finishAnimation;
    
    private GameObject _playerObject;
    [SerializeField] private GameObject[] _fireworks;
    private bool _firstTime = true;
    [SerializeField] private StartNextRun _startNextRun;
    private bool _isTutorial = true;

    private float _timer = 0f;
    
    private void Start()
    {
        _firstTime = true;
        _playerObject = GameObject.FindWithTag("Player");
        _finishAnimation = GameManager.Instance.CameraAnimator;
        _startNextRun = GameManager.Instance.CameraAnimator.gameObject.GetComponent<StartNextRun>();
    }

    private void Update()
    {
        if (!_firstTime && !_isTutorial)
        {
            _timer += Time.deltaTime;

            if (_timer >= 7f)
            {
                _startNextRun.StartNextRunEvent();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            if (_firstTime)
            {
                _finishAnimation.enabled = true;
                _finishAnimation.Rebind();
                _finishAnimation.enabled = false;
                _finishAnimation.enabled = true;
                
                AudioManager.GetInstance().SetAudio(SOUND_TYPE.VICTORY);
                _firstTime = false;
                for (int i = 0; i < _fireworks.Length; i++)
                {
                    _fireworks[i].SetActive(true);
                }
            }
            
            if (!GameManager.Instance.IsTutorial)
            {
                _isTutorial = false;
                GameManager.Instance.AddScore(1);
                Destroy(other.gameObject);
                GameObject particleSystem = Instantiate(_particleSystem, other.transform.position, other.transform.rotation);
                particleSystem.transform.SetParent(_playerObject.transform);
                AudioManager.GetInstance().SetAudio(SOUND_TYPE.DELIVER);
            }
        }
    }
}

