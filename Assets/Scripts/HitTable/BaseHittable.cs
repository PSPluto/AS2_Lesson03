using UnityEngine;

[CreateAssetMenu(fileName = "BaseHittable", menuName = "Scriptable Objects/BaseHittable")]
public class BaseHittable : ScriptableObject
{
    public int[] hitPattern;
    public int score;
}
