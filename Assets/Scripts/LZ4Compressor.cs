using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.InteropServices;
using System;
using System.Text;

public class LZ4Compressor : MonoBehaviour
{
    [SerializeField] private ImageInputData imgInputData;
    [SerializeField] private ActionAlarm alarm;

    private List<FileInfo> fileInfos = new List<FileInfo>();    

    #region Public Method
    public void Compress()
    {
        ScanFolder(imgInputData.PathForPNG);

        for (int i = 0; i < fileInfos.Count; i++)
        {
            if (fileInfos[i].Extension.Equals(".png"))
            {
                byte[] compressedData = lz4.Compress(File.ReadAllBytes(imgInputData.PathForPNG+ fileInfos[i].Name));
                File.WriteAllBytes(imgInputData.PathForLZ4 + Path.GetFileNameWithoutExtension(fileInfos[i].Name) + "_compressed.lz4", compressedData);
            }
        }
        alarm.Alarm("Compression is end.");
    }
    public void DeCompress()
    {
        ScanFolder(imgInputData.PathForLZ4);

        string targetString = "_compressed";

        for (int i = 0; i < fileInfos.Count; i++)
        {
            if (fileInfos[i].Extension.Equals(".lz4"))
            {
                string decompressedFileName = Path.GetFileNameWithoutExtension(fileInfos[i].Name);

                decompressedFileName = decompressedFileName.Replace(targetString, "");

                byte[] decompressedData = lz4.Decompress(File.ReadAllBytes(imgInputData.PathForLZ4 + Path.GetFileNameWithoutExtension(fileInfos[i].Name) + ".lz4"));
                File.WriteAllBytes(imgInputData.PathForPNG + decompressedFileName + ".png", decompressedData);
            }
        }
        alarm.Alarm("Decompression is end.");
    }
    #endregion

    #region Private Method
    private void ScanFolder(string path)
    {
        fileInfos.Clear();
        if (Directory.Exists(path))
        {
            DirectoryInfo di = new DirectoryInfo(path);

            foreach (var item in di.GetFiles())
            {
                fileInfos.Add(item);
            }
        }
    }
    #endregion
}
