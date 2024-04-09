using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static bool Level1Complete;
    public static bool Level2Complete;
    public static bool Level3Complete;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        Level1Complete = false;
        Level2Complete = false;
        Level3Complete = false;
        //Level1Complete = true;
        //Level2Complete = true;
        //Level3Complete = true;
    }

    public static void MarkLevel1Complete()
    {
        Level1Complete = true;
        print("Level1Complete = true");
    }

    public static void MarkLevel2Complete()
    {
        Level2Complete = true;
        print("Level2Complete = true");
    }

    public static void MarkLevel3Complete()
    {
        Level3Complete = true;
        print("Level3Complete = true");
    }
}
