using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class TiltLogic : MonoBehaviour, TiltActions.ITIltActions
{
    [SerializeField] private Transform trayTransform;
    [SerializeField, Range(0.1f, 0.99f)] private float _swingTreshold = 0.7f;
    [SerializeField] private MoveAlongPath _moveAlongPath;
    
    private float _previousRotateDirZ = 0;
    private float _previousRotateDirX = 0;
    private bool _canSwingDirZ = true;
    private bool _canSwingDirX = true;
    private Vector3 directionRot;
    private TiltActions.ITIltActions _itIltActionsImplementation;
    private Vector3 _movementValue;
    private TiltActions _tiltActions;

    private void Start()
    {
        _tiltActions = new TiltActions();
        _tiltActions.TIlt.SetCallbacks(this);
        _tiltActions.TIlt.Enable();
    }
    
    public void RotateLeftRight(float rotateDir)
    {
        if (PlayerPrefs.GetInt("InvertAxisX",0)==1)
        {
            rotateDir *=-1;
        }
        
        if (Mathf.Abs(rotateDir) >= _swingTreshold && _canSwingDirZ)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.SWING);
            _canSwingDirZ = false;
        }
       
        if (!_canSwingDirZ)
        {
            _canSwingDirZ = rotateDir * _previousRotateDirZ <= 0; // Reset when direction changes
        }
        
        directionRot.z =  rotateDir;
        _previousRotateDirZ = rotateDir;
    }
    
    public void RotateFrontBack(float rotateDir)
    {
        if (PlayerPrefs.GetInt("InvertAxisY",0)!=1)
        {
            rotateDir *=-1;
        }   
        
        if (Mathf.Abs(rotateDir) >= _swingTreshold && _canSwingDirX)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.SWING);
            _canSwingDirX = false;
        }
        
        if (!_canSwingDirX)
        {
            _canSwingDirX = rotateDir * _previousRotateDirX <= 0; // Reset when direction changes
        }
       
        directionRot.x = rotateDir;
        _previousRotateDirX = rotateDir;
    }

    void Update()
    {
        if (GameManager.Instance.GameState is GameState.gaming)
        {
            Quaternion angles = trayTransform.rotation; 
            Quaternion lockRotation = trayTransform.localRotation;
            switch (GameManager.Instance.GyroActive)
            {
                case true:
                    trayTransform.Rotate(directionRot * (GameManager.Instance.GameValues.TiltControlSpeed * 10 * Time.deltaTime), Space.Self);
            
                    if (angles.x > 0.211f  || angles.x < -0.211f) {
                        float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f);
                        angles.x = targetX;
                    }
            
                    if (angles.z > 0.211f || angles.z < -0.211f) {
                        float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                        angles.z = targetZ;
                    }
            
                    trayTransform.rotation = angles;

                    lockRotation.y = 0;

                    trayTransform.localRotation = lockRotation;
            
                    if (_moveAlongPath.StallTimer <= _moveAlongPath.StallTime)
                    {
                        trayTransform.localRotation = Quaternion.identity;
                    }
                    break;
                case false:
                    trayTransform.Rotate(directionRot * (GameManager.Instance.GameValues.TiltControlSpeed * 10 * Time.deltaTime), Space.Self);
            
                    if (angles.x > 0.211f  || angles.x < -0.211f) {
                        float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f);
                        angles.x = targetX;
                    }
            
                    if (angles.z > 0.211f || angles.z < -0.211f) {
                        float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                        angles.z = targetZ;
                    }
            
                    trayTransform.rotation = angles;

                    lockRotation.y = 0;

                    trayTransform.localRotation = lockRotation;
            
                    if (_moveAlongPath.StallTimer <= _moveAlongPath.StallTime)
                    {
                        trayTransform.localRotation = Quaternion.identity;
                    }
                    break;
            }
        }
    }
    
    private void OnDestroy()
    {
        _tiltActions.TIlt.Disable();
    }

    public void OnTiltGyro(InputAction.CallbackContext context)
    {
        _movementValue = context.ReadValue<Vector3>();
    }
}
