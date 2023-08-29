using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConquestTextLogger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] conquestLogText;
    [SerializeField] private ConquestMessageLog CML;

    public void UpdateConquestLog()
    {
        conquestLogText[0].text = CML.conquestLogString[0];
        conquestLogText[1].text = CML.conquestLogString[1];
        conquestLogText[2].text = CML.conquestLogString[2];
    }
}
