using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Stats/ActorStats", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private Stats _stats;

    public int MaxLife => _stats.MaxLife;
    public float MovementSpeed => _stats.MovementSpeed;

    public float JumpForce => _stats.JumpForce;

}

[System.Serializable]
public struct Stats
{
    public int MaxLife;
    public float MovementSpeed;
    public float JumpForce;

}
