using System;
using UnityEngine;

public class DeliverObject : MonoBehaviour
{
    public GameObject _particleSystem;

    [SerializeField] private GameObject[] _fireworks;
    [SerializeField] private AnimationCurve _animationCurve;
    
    private Animator _finishAnimation;
    private GameObject _playerObject;
    private StartNextRun _startNextRun;
    private Transform _cameraTransform;
    private bool _isTutorial = true;
    private bool _firstTime = true;
    private float _timer = 0f;
    
    private void Start()
    {
        _firstTime = true;
        _playerObject = GameObject.FindWithTag("Player");
        _finishAnimation = GameManager.Instance.CameraAnimator;
        _startNextRun = GameManager.Instance.CameraAnimator.gameObject.GetComponent<StartNextRun>();
        _cameraTransform = GameManager.Instance.CameraAnimator.transform;
    }

    private void Update()
    {
        if (!_firstTime && !_isTutorial)
        {
            if(GameManager.Instance.GameState == GameState.gaming)
                _timer += Time.deltaTime;

            Quaternion rotation;
            rotation = _cameraTransform.localRotation;
            rotation.eulerAngles = new Vector3(_animationCurve.Evaluate(_timer), rotation.eulerAngles.y, rotation.eulerAngles.z);
            _cameraTransform.localRotation = rotation;
            
            if (_timer >= 5f)
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
                GameManager.Instance.DisableDistance();
                
                if (GameManager.Instance.IsTutorial)
                {
                    _finishAnimation.enabled = true;
                    _finishAnimation.Rebind();
                    _finishAnimation.enabled = false;
                    _finishAnimation.enabled = true;  
                }
                
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

