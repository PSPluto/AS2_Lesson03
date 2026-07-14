using UnityEngine;
[CreateAssetMenu(fileName = "DoubleNumbersPattern", menuName = "Scriptable Objects/DoubleNumbersPattern")]


// 横の列の数字がすべて同じ場合にスコアを返す

public class DoubleNumbersPattern : BaseHittable
{
    [SerializeField] private int rowIndex; //チェック行の番号
    public override int checkPattern(int[,] slot)
    {
        int[] row = ProjectUtility.GetRow(slot, rowIndex);
        int lastNum = row[0];

        foreach (int num in row)
        {
            if (num == lastNum)
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
