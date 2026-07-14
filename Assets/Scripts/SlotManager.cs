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

    public AudioSource audioSource;
    [SerializeField]private AudioClip leverSound;
    [SerializeField]private AudioClip startSound;
    [SerializeField]private AudioClip stopSound;

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
                audioSource.PlayOneShot(stopSound);
            }
            else
            {
                StartCoroutine(SlotUpdate());
                audioSource.PlayOneShot(leverSound);
                // 前回当たってなったらコレ
                //audioSource.PlayOneShot(startSound);
                // 当たってたらもっと明るいやつ鳴らす
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
            rilleObjects[j].transform.rotation = Quaternion.Lerp(rilleObjects[j].transform.rotation, Quaternion.Euler(rilleAngle[j], 0, 0), 0.1f);
        }
    }


    void InitRille()
    {
        rilleStartIndex = 0;
        rille = new int[,]
        {
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


    IEnumerator SlotUpdate()
    {
        isplayingSlot = true;
        InitRille();
        while (true)
        {
            PlayingSlot();
            yield return new WaitForSeconds(0.05f);
            if (rilleStartIndex == rille.GetLength(1))
            {
                Debug.Log(string.Join(", ", ProjectUtility.GetRow(rille, 1)));
                Debug.Log(PatternCheck(rille));
                isplayingSlot = false;
                yield break;
            }
        }
    }
    private int PatternCheck(int[,] slot)
    {
        int resultScore = 0;
        // テーブル分繰り返す
        foreach (BaseHittable obj in hitTable)
        {
              resultScore += obj.checkPattern(slot);
        }
        return resultScore;
    }
}