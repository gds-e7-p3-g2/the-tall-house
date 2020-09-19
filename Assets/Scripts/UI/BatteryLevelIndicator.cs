using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryLevelIndicator : MonoBehaviour
{
    public void DisplayBatteryLevel(float level)
    {
        GetComponent<Text>().text = level.ToString("#.0") + "%";
    }
}
