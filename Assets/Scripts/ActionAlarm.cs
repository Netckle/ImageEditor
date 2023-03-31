using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ActionAlarm : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI messageTMP;

    public void Alarm(string message)
    {
        messageTMP.text = message;
    }
}
