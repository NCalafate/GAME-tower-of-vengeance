using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackAccuracyScriptableObject", menuName = "AttackAccuracyScriptableObject")]

public class AttackAccuracyScriptableObject : ScriptableObject
{
    [SerializeField] private LayerMask _humanoidLayer;
    [SerializeField] private float _humanoid = 0.15f;
    [SerializeField] private LayerMask _obstaclesLayer;
    [SerializeField] private float _obstacles = 0.3f;
    [SerializeField] private LayerMask _solidLayer;
    [SerializeField] private float _solid = 1.0f;

    public LayerMask HumanoidLayer => _humanoidLayer;
    public float Humanoid => _humanoid;
    public LayerMask ObstaclesLayer => _obstaclesLayer;
    public float Obstacles => _obstacles;
    public LayerMask SolidLayer => _solidLayer;
    public float Solid => _solid;
}