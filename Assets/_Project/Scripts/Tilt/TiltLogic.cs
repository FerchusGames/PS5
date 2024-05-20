using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TiltLogic : MonoBehaviour
{
    [SerializeField] private Transform trayTransform;
    [SerializeField, Range(0.1f, 0.99f)] private float _swingTreshold = 0.7f;

    public float speed = 100f;
    private float _previousRotateDirZ = 0;
    private float _previousRotateDirX = 0;
    private bool _canSwingDirZ = true;
    private bool _canSwingDirX = true;
    private float _swingAudioLevel = 0.4f;
    private Vector3 directionRot;

    
    public void RotateLeftRight(float rotateDir)
    {
        if (Mathf.Abs(rotateDir) >= _swingTreshold && _canSwingDirZ)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.SWING, _swingAudioLevel);
            _canSwingDirZ = false;
        }

        if (!_canSwingDirZ)
        {
            _canSwingDirZ = rotateDir * _previousRotateDirZ <= 0; // Reset when direction changes
        }
        
        if (PlayerPrefs.GetInt("InvertAxisX",0)==1)
        {
            rotateDir *=-1;
        }
       
        directionRot.z =  rotateDir;
        _previousRotateDirZ = rotateDir;
    }
    
    public void RotateFrontBack(float rotateDir)
    {
        if (Mathf.Abs(rotateDir) >= _swingTreshold && _canSwingDirX)
        {
            AudioManager.GetInstance().SetAudio(SOUND_TYPE.SWING, _swingAudioLevel);
            _canSwingDirX = false;
        }
        
        if (!_canSwingDirX)
        {
            _canSwingDirX = rotateDir * _previousRotateDirX <= 0; // Reset when direction changes
        }
        
        if (PlayerPrefs.GetInt("InvertAxisY",0)==1)
        {
            rotateDir *=-1;
        }   
        directionRot.x = rotateDir;
        _previousRotateDirX = rotateDir;
    }

    void Update()
    {
        if (GameManager.Instance.GameState is GameState.gaming)
        {
            trayTransform.Rotate(directionRot * (speed * Time.deltaTime), Space.Self);
            Quaternion angles = trayTransform.rotation; 
            
            if (angles.x > 0.211f  || angles.x < -0.211f) {
                float targetX = Mathf.Clamp(angles.x, -0.211f, 0.211f);
                angles.x = targetX;
            }
            
            if (angles.z > 0.211f || angles.z < -0.211f) {
                float targetZ = Mathf.Clamp(angles.z, -0.211f, 0.211f);
                angles.z = targetZ;
            }
            
            trayTransform.rotation = angles;
        }
    }
}
