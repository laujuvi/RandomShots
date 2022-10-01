using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Stats/GunStats", order = 0)]
public class GunStats : ScriptableObject
{
    [SerializeField] private Gun_Stats _stasts;

    public UnityEngine.GameObject BulletPrefab => _stasts.BulletPrefab;
    public int BulletDamage => _stasts.BulletDamage;
    public float BulletSpeed => _stasts.BulletSpeed;
    public float Cooldown => _stasts.Cooldown;
    public int MagSize => _stasts.MagSize;
    public float BulletLifeTime => _stasts.BulletLifeTime;
}

[System.Serializable]
public struct Gun_Stats
{
    public int BulletDamage;
    public float BulletSpeed;
    public float BulletLifeTime;
    public float Cooldown;
    public int MagSize;
    public UnityEngine.GameObject BulletPrefab;
}