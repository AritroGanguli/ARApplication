using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class TakePhoto : MonoBehaviour{ 

    // public
    // private
    private bool isProcessing = false;
    public Image buttonShare;
    public Image buttonQuit;
    public string message;

    //function called from a button
    public void CaptureShot()
    {
        buttonShare.enabled = false;
        buttonQuit.enabled = false;
        if (!isProcessing)
        {
            StartCoroutine(TakeShot());
        }
    }
    public IEnumerator TakeShot()
    {
        isProcessing = true;
        // wait for graphics to render
        yield return new WaitForEndOfFrame();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        // create the texture
        Texture2D screenTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        // put buffer into texture
        screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        // apply
        screenTexture.Apply();
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- PHOTO
        var name = System.DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg";
        byte[] dataToSave = screenTexture.EncodeToJPG();
        string destination = Path.Combine(Application.persistentDataPath, name);
        File.WriteAllBytes(destination, dataToSave);

        NativeGallery.SaveImageToGallery(destination, "Onyo Pujo", name, null);

        //if (!Application.isEditor)

        /*{
            // block to open the file and share it ------------START
            AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
            AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
            AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject);

            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), "" + message);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), "SUBJECT");

            intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");

            currentActivity.Call("startActivity", intentObject);
        }*/
        isProcessing = false;
        buttonShare.enabled = true;
        buttonQuit.enabled = true;
    }
}