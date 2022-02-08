using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Windows;
// using UnityEditor;
using System.IO;


public class ScreenshotScript : MonoBehaviour
{

    public GameObject[] gameObjectsToHide;
    public float delay;
    public Index index;

    [Header("Cut Screenshot")]
    public int offX;
    public int offY;
    public bool symmetricalCut;
    public int width, height;
    public int scale;

    string rmbFileName = "none";
    // System.Random rand = new System.Random();
    


    void Awake(){
        // TakeScreenshot();
        offX = offX * scale;
        offY = offY * scale;
    }

    public void TakeScreenshot(){
        if(index.dataScript.rmbFileName == "" || index.dataScript.rmbFileName == null){
            rmbFileName = index.dataScript.fileFolderPath +"\\"+ "empty";
        } else
        rmbFileName = index.dataScript.fileFolderPath +"\\"+ index.dataScript.rmbFileName;
        StartCoroutine(Screenshot());
    }

    private IEnumerator Screenshot(){
        foreach(GameObject go in gameObjectsToHide){
            go.SetActive(false);
        }
        // yield return new WaitForSeconds(delay);

        // height = Screen.height;
        // width = Screen.width;
        // Rect cut = new Rect(offX, offY, width, height);
        // Texture2D texture = new Texture2D(width, height, TextureFormat.RGB24, false);
        // texture.ReadPixels(cut, 0, 0, false);
        // byte[] bytes = texture.EncodeToPNG();
        // System.IO.File.WriteAllBytes("output.png", bytes);


        bool isInsta = index.menuScript.IsInstagramPost();
        Debug.Log(rmbFileName);
        int pngWidth = 1920/2 * scale;
        int pngHeight = 1080/2 * scale;
        if(symmetricalCut){
            width = 1920 - offX * 2;
            height = 1080 - offY * 2;
        }

        // int key = rand.Next(0, 1000000000);

        ScreenCapture.CaptureScreenshot(rmbFileName + ((isInsta)?"_insta":"") + ".png", scale);
        // ScreenCapture.CaptureScreenshot("testoutput.png", scale);

        yield return new WaitForSeconds(delay);

        byte[] bytes = File.ReadAllBytes(rmbFileName + ((isInsta)?"_insta":"") + ".png");
        Texture2D texture = new Texture2D(pngWidth, pngHeight);
        texture.LoadImage(bytes);

        RectInt cropRect = new RectInt(offX, offY, width, height);

        byte[] bytes2 = CropTexture(texture, cropRect).EncodeToPNG();

        
        System.IO.File.WriteAllBytes(rmbFileName + ((isInsta)?"_insta":"") +".png", bytes2);

        yield return new WaitForSeconds(delay);

        // File.Delete(index.dataScript.fileFolderPath+"\\"+key+".png");


        Debug.Log(rmbFileName);
        foreach(GameObject go in gameObjectsToHide){
            go.SetActive(true);
        }
    }

    Texture2D CropTexture(Texture2D input, RectInt rect){
        Texture2D texture = new Texture2D(rect.width, rect.height);
        Color[,] color = new Color[rect.width, rect.height];

        for(int ix = 0; ix < rect.width; ix++){
            for(int iy = 0; iy < rect.height; iy++){
                color[ix, iy] = input.GetPixel(rect.xMin + ix, rect.yMin + iy);
                texture.SetPixel(ix, iy, color[ix, iy]);
            }
        }
        
        texture.Apply();
        Debug.Log(texture.width + " | "+ texture.height);
        return texture;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
    }
}
