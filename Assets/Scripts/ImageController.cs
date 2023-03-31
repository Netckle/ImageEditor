using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Wing.uPainter;
using TMPro;

public class ImageController : MonoBehaviour
{
    [SerializeField] private RawImage img;
    [SerializeField] private ImageInputData imgInputData;
    [SerializeField] private ActionAlarm alarm;

    private RectTransform imgRect;
    private Texture2D imgTex2D;
    private Texture2D scaledImgTex2D;

    private RawImagePaintCanvas imgPaintCanvas;

    #region # Unity Method
    private void Awake()
    {
        imgRect = img.GetComponent<RectTransform>();
        imgPaintCanvas = img.GetComponent<RawImagePaintCanvas>();
    }
    #endregion

    #region # Public Method
    public void ScaleImage()
    {
        scaledImgTex2D = ScaleTexture(imgTex2D, imgInputData.ScaleFactor);
    }
    public void WriteImage()
    {
        WriteTexture(imgInputData.SaveWithScaledImage);
    }
    public void LoadImage()
    {
        StartCoroutine(WebRequest());
    }
    #endregion

    #region # Private Method
    private Texture2D ScaleTexture(Texture2D source, float scaleFactor = 1.0f)
    {
        if (scaleFactor == 1f)
        {
            return source;
        }
        else if (scaleFactor == 0f)
        {
            return Texture2D.blackTexture;
        }

        int width = Mathf.RoundToInt(source.width * scaleFactor);
        int height = Mathf.RoundToInt(source.height * scaleFactor);

        Color[] _scaledTexPixels = new Color[width * height];

        for (int yCord = 0; yCord < height; yCord++)
        {
            float vCord = yCord / (height * 1f);
            int scanLineIndex = yCord * width;

            for (int xCord = 0; xCord < width; xCord++)
            {
                float uCord = xCord / (width * 1f);

                _scaledTexPixels[scanLineIndex + xCord] = source.GetPixelBilinear(uCord, vCord);
            }
        }

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(_scaledTexPixels, 0);
        result.Apply();

        alarm.Alarm("Current scale is " + imgInputData.ScaleFactor.ToString()); 

        return result;
    }
    // uPainter을 위한 rawImage 저장 기능입니다.
    private void WriteTexture(bool withScaledImage = false)
    {
        if (false == Directory.Exists(imgInputData.PathForSave))
        {
            Directory.CreateDirectory(imgInputData.PathForSave);
        }       
        string filePath = imgInputData.PathForSave + imgInputData.FileName + ".png";
        imgPaintCanvas.Layers[0].Save(filePath, 1024, 512, EPictureType.PNG);

        if (withScaledImage)
        {

        }

        alarm.Alarm("Save is End");
    }
    private IEnumerator WebRequest()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgInputData.URL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            alarm.Alarm(www.error);
        }
        else
        {
            imgTex2D = ((DownloadHandlerTexture)www.downloadHandler).texture;
            yield return ResizeTextureToScreen();
            img.texture = imgTex2D;

            imgPaintCanvas.Initial();

            alarm.Alarm("Load is End");
        }
    }
    private IEnumerator ResizeTextureToScreen()
    {
        float width = imgTex2D.width;
        float height = imgTex2D.height;

        while (height > Screen.height / 2.5)
        {
            width *= 0.95f;
            height *= 0.95f;
            yield return null;
        }
        imgRect.sizeDelta = new Vector2(width, height);
    }
    #endregion
}
