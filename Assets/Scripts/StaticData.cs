using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour {
    private enum BodyPartNum {
            // test,
            ecum, fcum, tri, tam, del,
            back, quad, pat, gas, ham
    };

    private static string[] bodyPartName = { 
        "ecum", "fcum", "tri", "tam", "del",
        "back", "quad", "pat", "gas", "ham"
    };
    // initial distance == 0.08 (simulator)
    private static double[] LengthArray = { 0.2, 0.21, 0.22, 0.23, 0.24, 0.25, 0.26, 0.27, 0.28, 0.29, 0.30 };
    private static double TargetTapeLength;
    private static bool isCut = false;
    private static bool showScissor = false;
    private static int targetNum;

    private static int curTapingStep = 0;
    private static bool tapeAttachLeftHand = false;
    private static bool tapeAttachBothHands = false;


    public static void setTargetTapeLength (string btn_name)
    {
        BodyPartNum parsed_enum = (BodyPartNum) System.Enum.Parse(typeof(BodyPartNum),  btn_name);
        
        // set up the targetNum
        targetNum = (int)parsed_enum;

        // set up target tape length
        TargetTapeLength = LengthArray[(int) parsed_enum];
        Debug.Log(TargetTapeLength);

        // set up target animation model
        
        
    }

    public static void setCurTapingStep(int step) {
        curTapingStep = step;
        Debug.Log("current step: " + curTapingStep);
    }

    public static int getCurTapingStep() {
        return curTapingStep;
    }

    public static int getTargetNum()
    {
        return targetNum;
    }

    public static string getTargetNumName()
    {
        // Debug.Log(bodyPartName[targetNum]);
        return bodyPartName[targetNum];
    }

    public static double getTargetTapeLength ()
    {
        return TargetTapeLength = 0.2;
    }

    public static List<string> getTargetBodyCollider ()
    {
        BodyPartNum part = BodyPartNum.ecum;//(BodyPartNum)targetNum;
        List<string> ret = new List<string>();
        switch (part)
        {
            case (BodyPartNum.ecum):
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/Bip001 L Hand/L_wrist_1");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 L Clavicle/Bip001 L UpperArm/Bip001 L Forearm/L_wrist_2");
                break;
            case (BodyPartNum.back):
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Top_L");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Top_M");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Top_R");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Bot_1");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Bot_2");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Bot_3");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Bot_4");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Back_Bot_5");
                break;
            case (BodyPartNum.pat):
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 L Thigh/Pat_L_1");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 L Thigh/Pat_L_2");
                break;
            case (BodyPartNum.gas):
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 L Thigh/Bip001 L Calf/Gas_L_1");
                ret.Add("/playerCollider/Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 L Thigh/Bip001 L Calf/Gas_L_2");
                break;
            default:
                break;
        }
        return ret;
    }


    public static void setIsCut (bool value)
    {
        isCut = value;
    }

    public static void setShowScissor(bool value)
    {
        showScissor = value;
    }

    public static void resetAll() {
        showScissor = false;
        isCut = false;
        tapeAttachLeftHand = false;
        tapeAttachBothHands = false;
        curTapingStep = 0;
    }
    public static bool getShowScissor()
    {
        return showScissor;
    }

    public static bool getIsCut()
    {
        return isCut;
    }

    public static void setTapeAttachLeftHand(bool value) {
        tapeAttachLeftHand = value;
    }

    public static void setTapeAttachBothHands(bool value) {
        tapeAttachBothHands = value;
    }

    public static bool isTapeAttachLeftHand() {
        return tapeAttachLeftHand;
    }

    public static bool isTapeAttachBothHands() {
        return tapeAttachBothHands;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}