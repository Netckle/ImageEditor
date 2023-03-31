using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialController : MonoBehaviour
{
    public enum Part { All, Head, Body, ArmAndLeg };
    public Part partToChange;

    Renderer renderer = null;

    private void Awake()
    {
        renderer = obj.GetComponent<Renderer>();
    }

    public void Change(Material material, Texture2D tex, int partNumber)
    {
        switch((Part)partNumber)
        {
            case Part.All:
                material.SetTexture("_Head_Map", tex);
                material.SetTexture("_Body_Map", tex);
                material.SetTexture("_ArmAndLeg_Map", tex);
                break;
            case Part.Head:
                material.SetTexture("_Head_Map", tex);
                break;
            case Part.Body:
                material.SetTexture("_Body_Map", tex);
                break;
            case Part.ArmAndLeg:
                material.SetTexture("_ArmAndLeg_Map", tex);
                break;
        }
    }

    public GameObject obj;
}
