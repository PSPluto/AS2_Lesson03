using UnityEngine;

public static class ProjectUtility
{
    public static int[] GetRow(int[,] sourceArray, int rowIndex)
    {
        int colCount = sourceArray.GetLength(1);
        int[] result = new int[colCount];

        for (int i = 0; i < colCount; i++)
        {
            result[i] = sourceArray[rowIndex, i];
        }

        return result;
    }
}
