using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ImageInputData", menuName = "Scriptable Object/Image Input Data", order = int.MaxValue)]
public class ImageInputData : ScriptableObject
{
    [SerializeField]
    private string url;
    [SerializeField]
    private string fileName;
    [SerializeField]
    private string pathForSave;
    [SerializeField]
    private string pathForPNG;
    [SerializeField]
    private string pathForLZ4;
    [SerializeField]
    [Range(0, 1)]
    private float scaleFactor = 0.0f;
    [SerializeField]
    private bool saveWithScaledImage = false;
    public string URL { get { return url; } }
    public string FileName { get { return fileName; } }
    public string PathForSave { get { return pathForSave; } }
    public string PathForPNG { get { return pathForPNG; } }
    public string PathForLZ4 { get { return pathForLZ4; } }
    public float ScaleFactor { get { return scaleFactor; } }
    public bool SaveWithScaledImage { get { return saveWithScaledImage; } }
}
