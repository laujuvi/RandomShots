using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Stats/ActorStats", order = 0)]
public class ActorStats : ScriptableObject
{
    [SerializeField] private Stats _stats;

    public int MaxLife => _stats.MaxLife;
    public float MovementSpeed => _stats.MovementSpeed;
}

[System.Serializable]
public struct Stats
{
    public int MaxLife;
    public float MovementSpeed;
}
