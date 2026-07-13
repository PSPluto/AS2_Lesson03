using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// スロット管理
public class SlotManager : MonoBehaviour
{
    private int[,] rille = new int[5, 3];
    private int rilleStartIndex = 0;
    private float[] rilleAngle;
    public bool isplayingSlot = false;
    [SerializeField] private ScriptableObject[] hitTable;
    [SerializeField] private GameObject[] rilleObjects;

    void Start()
    {
        StartCoroutine(SlotUpdate());
    }
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (isplayingSlot == true)
            {
                rilleStartIndex++;
                rilleStartIndex = Mathf.Clamp(rilleStartIndex, 0, rille.GetLength(1));
            }
            else
            {
                StartCoroutine(SlotUpdate());
            }


        }
        UpdateRilleObjects();
    }

    void PlayingSlot()
    {
        // for：
        // (初期化式; 条件式; 更新式;)
        for (int i = rilleStartIndex; i < rille.GetLength(1); i++)
        {
            RotateRille(i);
            //Debug.Log(rille[1, i]);

        }
    }

    void UpdateRilleObjects()
    {
        for (int j = 0; j < rille.GetLength(1); j++)
        {
            rilleAngle[j] = (360 / rille.GetLength(0)) * rille[1, j];
            rilleObjects[j].transform.rotation = Quaternion.Lerp(rilleObjects[j].transform.rotation, Quaternion.Euler(rilleAngle[j], 0, 0), 0.05f);
        }
    }


    void InitRille()
    {
        rilleStartIndex = 0;
        rille = new int[,]
        {
            {0, 0, 0},
            {1, 1, 1},
            {2, 2, 2},
            {3, 3, 3},
            {4, 4, 4},
            {5, 5, 5}
        };
        rilleAngle = new float[rille.GetLength(1)];
    }
    void RotateRille(int line, bool CCW = false)
    {
        // CCW  : 反時計回り
        // temp : 一時保存用
        // GetLength(1)配列の行の長さを取得できる（ーーー横）
        // Length(0)配列全体の要素(element)数を取得できる（｜｜｜縦）
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
    public int[] GetRow(int[,] sourceArray, int rowIndex)
    {
        int colCount = sourceArray.GetLength(1);
        int[] result = new int[colCount];

        for (int i = 0; i < colCount; i++)
        {
            result[i] = sourceArray[rowIndex, i];
        }

        return result;
    }

    IEnumerator SlotUpdate()
    {
        isplayingSlot = true;
        InitRille();
        while (true)
        {
            PlayingSlot();
            yield return new WaitForSeconds(0.1f);
            if (rilleStartIndex == rille.GetLength(1))
            {
                Debug.Log(string.Join(", ", GetRow(rille, 1)));
                Debug.Log(PatternCheck(rille));
                isplayingSlot = false;
                yield break;
            }
        }
    }
    private int PatternCheck(int[,] slot)
    {
        // 繰り返す
        foreach (BaseHittable obj in hitTable)
        {
            Debug.Log(string.Join(", ", GetRow(slot, 1)));
            Debug.Log(string.Join(", ", obj.hitPattern));
            // 同じかどうかの判定
            for (int n = 0; n <= slot.GetLength(1); n++)
            {
                if (obj.hitPattern[n] != GetRow(slot, 1)[n])
                {
                    return 0;
                }
            }

            Debug.Log("!!");
            return obj.score;
        }
        return 0;
    }
}