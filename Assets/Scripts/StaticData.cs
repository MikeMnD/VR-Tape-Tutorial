﻿using System.Collections;
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
    public static void setTargetTapeLength (string btn_name)
    {
        BodyPartNum parsed_enum = (BodyPartNum) System.Enum.Parse(typeof(BodyPartNum),  btn_name);
        TargetTapeLength = LengthArray[(int) parsed_enum];
        Debug.Log(TargetTapeLength);
    }

    public static double getTargetTapeLength ()
    {
        return TargetTapeLength = 0.2;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
