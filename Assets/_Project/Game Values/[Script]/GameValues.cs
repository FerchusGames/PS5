using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Values", fileName = "Game Values")]
public class GameValues : ScriptableObject
{
    [Header("Movement")]
    [Range(1f, 40f), Tooltip("The maximum speed at which the player will move during gameplay")] 
    public float MaxSpeed = 10f;

    [Header("Control")]
    [Range(0.1f, 20f), Tooltip("The speed at which the tray rotates when the player moves the joysticks")]
    public float TiltControlSpeed = 2f;
    
    [Header("Tray Balancing")]
    [Range(0.1f, 20f), Tooltip("The rate at which the tray will tilt left/right when the player is turning")]
    public float TurningTiltRate = 5f;
    [Range(0.1f, 20f), Tooltip("The rate at which the tray will tilt because of the objects' weight")]
    public float WeightTiltRate = 6f;
    
    [Header(("Spawn"))] 
    [Range(4f, 10f), Tooltip("The minimum amount of objects that can spawn in the tray")] 
    public int MinObjects = 5;
    [Range(4f, 10f), Tooltip("The maximum amount of objects that can spawn in the tray")] 
    public int MaxObjects = 8;
}
