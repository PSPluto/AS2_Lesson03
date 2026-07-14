using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "BaseHittable", menuName = "Scriptable Objects/BaseHittable")]
public class BaseHittable : ScriptableObject
{
    public int score;
    public virtual int checkPattern(int[,] slot)
    {
        return 0;
    }
}
