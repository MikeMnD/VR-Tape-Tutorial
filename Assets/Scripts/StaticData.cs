using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour {
    private enum BodyPartNum {
            test,
            ecum, fcum, tri, tam, del,
            back, quad, pat, gas, ham
    };
    // initial distance == 0.08 (simulator)
    private static double[] LengthArray = { 0.2, 0.21, 0.22, 0.23, 0.24, 0.25, 0.26, 0.27, 0.28, 0.29, 0.30 };
    private static double TargetTapeLength;
    private static bool isCut = false;
    private static bool showScissor = false;
    private static int targetNum;

    public static void setTargetTapeLength (string btn_name)
    {
        BodyPartNum parsed_enum = (BodyPartNum) System.Enum.Parse(typeof(BodyPartNum),  btn_name);
        targetNum = (int)parsed_enum;

        // set up target tape length
        TargetTapeLength = LengthArray[(int) parsed_enum];
        Debug.Log(TargetTapeLength);

        // set up target animation model
        
        
    }

    public static int getTargetNum()
    {
        return targetNum;
    }

    public static double getTargetTapeLength ()
    {
        return TargetTapeLength = 0.2;
    }

    public static void setIsCut (bool value)
    {
        isCut = value;
    }

    public static void setShowScissor(bool value)
    {
        showScissor = value;
    }

    public static bool getShowScissor()
    {
        return showScissor;
    }

    public static bool getIsCut()
    {
        return isCut;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
