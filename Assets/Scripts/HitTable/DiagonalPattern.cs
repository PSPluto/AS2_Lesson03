using UnityEngine;
[CreateAssetMenu(fileName = "DiagonalPattern", menuName = "Scriptable Objects/DiagonalPattern")]

// 横の列の数字がすべて同じ場合にスコアを返す

public class DiagonalPattern : BaseHittable
{
    [SerializeField] private bool Inbound; //右に向かって上がる(true)か下がる(false)か
    public override int checkPattern(int[,] slot)
    {
        if (Inbound)
        {
            int lastNum = slot[slot.GetLength(0) - 1, 0];

            for (int i = slot.GetLength(0)-1; i == 0; i--)
            {
                if (slot[0, i] == lastNum)
                {
                    continue;
                }
                else
                {
                    return 0;
                }
            }
            return score;
        }
        else
        {
            int lastNum = slot[0, 0];
            
            for (int i = 0; i == slot.GetLength(0) - 1; i++)
            {
                if (slot[i, i] == lastNum)
                {
                    continue;
                }
                else
                {
                    return 0;
                }
            } 
            return score;
        }

    }
}
