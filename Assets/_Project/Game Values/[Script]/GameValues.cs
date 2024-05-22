using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Values", fileName = "Game Values")]
public class GameValues : ScriptableObject
{
    [Header("Movement")]
    [Range(5, 40), Tooltip("The speed at which the tray rotates when the player moves the joysticks")] 
    public float MaxSpeed = 10f;

    [Header("Control")]
    [Range(5, 40), Tooltip("The speed at which the tray rotates when the player moves the joysticks")]
    public float TiltControlSpeed = 10f;
    
    [Header("Tray Balancing")]
    [Range(1, 20), Tooltip("The rate at which the tray will tilt left/right when the player is turning")]
    public float TurningTiltRate = 5f;
    [Range(1, 20), Tooltip("The rate at which the tray will tilt because of the objects' weight")]
    public float WeightTiltRate = 10f;
    
    [Header(("Spawn"))] 
    [Range(4, 10), Tooltip("The minimum amount of objects that can spawn in the tray")] 
    public int MinObjects = 5;
    [Range(4, 10), Tooltip("The maximum amount of objects that can spawn in the tray")] 
    public int MaxObjects = 8;
}
