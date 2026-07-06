using UnityEngine;

// スロット管理
public class SlotManager : MonoBehaviour
{
    int[,] rille = new int[5, 3];
    void Start()
    {
        InitRille();
    }
    void Update()
    {
        PlayingSlot();
    }

    void PlayingSlot()
    {
        // for：
        // (初期化式; 条件式; 更新式;)
        for (int i = 0; i < rille.GetLength(1); i++)
        {
            RotateRille(i);
            Debug.Log(rille[1, i]);
        }
    }


    void InitRille()
    {
        rille = new int[,]
        {
            {0, 0, 0},
            {1, 1, 1},
            {2, 2, 2},
            {3, 3, 3},
            {4, 4, 4},
            {5, 5, 5}
        };
    }
    void RotateRille(int line, bool CCW = false)
    {
        // CCW  : 反時計回り
        // temp : 一時保存用
        // GetLength(0)配列の行の長さを取得できる
        // Length(0)配列全体の要素(element)数
        int length = rille.GetLength(0);
        if (CCW == true)
        {
            int temp = rille[0, line];
            for (int i = 0; i < length - 1; i++)
            {
                rille[i, line] = rille[i + 1, line];
            }
            rille[length - 1, line] = temp;
        }
        else
        {
            int temp = rille[length - 1, line];
            for (int i = length - 1; i > 0; i--)
            {
                rille[i, line] = rille[i - 1, line];
            }
            rille[0, line] = temp;
        }
    }
}
