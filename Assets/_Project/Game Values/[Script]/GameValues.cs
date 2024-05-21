using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Values", fileName = "Game Values")]
public class GameValues : ScriptableObject
{
    [field:Header("Movement")]
    [field:SerializeField, Range(5, 40), Tooltip("The speed at which the tray rotates when the player moves the joysticks")] 
    public float MaxSpeed { get; private set; } = 10f;

    [field: Header("Control")]
    [field: SerializeField, Range(5, 40), Tooltip("The speed at which the tray rotates when the player moves the joysticks")]
    public float TiltControlSpeed { get; private set; } = 10f;
    
    [field: Header("Tray Balancing")]
    [field: SerializeField, Range(1, 20), Tooltip("The rate at which the tray will tilt left/right when the player is turning")]
    public float TurningTiltRate { get; private set; } = 5f;
    [field: SerializeField, Range(1, 20), Tooltip("The rate at which the tray will tilt because of the objects' weight")]
    public float WeightTiltRate { get; private set; } = 10f;
    
    [field:Header(("Spawn"))] 
    [field:SerializeField, Range(4, 10), Tooltip("The minimum amount of objects that can spawn in the tray")] 
    public int MinObjects { get; private set; } = 5;
    [field:SerializeField, Range(4, 10), Tooltip("The maximum amount of objects that can spawn in the tray")] 
    public int MaxObjects { get; private set; } = 8;
}
