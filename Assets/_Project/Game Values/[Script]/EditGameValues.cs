using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;

public class EditGameValues : MonoBehaviour
{
    [SerializeField] private ValueToAssign _valueToAssign;
    
    public void ChangeGameValue(float value)
    {
        switch (_valueToAssign)
        {
            case ValueToAssign.MaxSpeed:
                GameManager.Instance.GameValues.MaxSpeed = value;
                break;
            case ValueToAssign.TiltControlSpeed:
                GameManager.Instance.GameValues.TiltControlSpeed = value;
                break;
            case ValueToAssign.TurningTiltRate:
                GameManager.Instance.GameValues.TurningTiltRate = value;
                break;
            case ValueToAssign.WeightTiltRate:
                GameManager.Instance.GameValues.WeightTiltRate = value;
                break;
            case ValueToAssign.MinObjects:
                GameManager.Instance.GameValues.MinObjects = (int)value;
                break;
            case ValueToAssign.MaxObjects:
                GameManager.Instance.GameValues.MaxObjects = (int)value;
                break;
        }
    }
    
    public enum ValueToAssign
    {
        MaxSpeed,
        TiltControlSpeed,
        TurningTiltRate,
        WeightTiltRate,
        MinObjects,
        MaxObjects,
    }
}
