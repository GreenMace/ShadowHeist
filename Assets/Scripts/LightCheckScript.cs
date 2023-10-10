using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheckScript : MonoBehaviour {

    public RenderTexture lightCheckTexture;
    public float LightLevel;
    public int minLightLevel = 20000;
    public int maxLightLevel = 800000;

    // Update is called once per frame
    void Update() {
        RenderTexture tmpTexture = RenderTexture.GetTemporary(
            lightCheckTexture.width, 
            lightCheckTexture.height, 
            0, 
            RenderTextureFormat.Default, 
            RenderTextureReadWrite.Linear);

        Graphics.Blit(lightCheckTexture, tmpTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = tmpTexture;

        Texture2D tmp2DTexture = new Texture2D(lightCheckTexture.width, lightCheckTexture.height);

        tmp2DTexture.ReadPixels(new Rect(0, 0, tmpTexture.width, tmpTexture.height), 0, 0);
        tmp2DTexture.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(tmpTexture);

        Color32[] colors = tmp2DTexture.GetPixels32();
        Texture2D.Destroy(tmp2DTexture);

        LightLevel = 0;

        for(int i = 0; i < colors.Length;  ++i) {
            LightLevel += 0.2126f * colors[i].r + 0.7152f * colors[i].g + 0.0722f * colors[i].b;
        }

        LightLevel = Mathf.Min(1,(LightLevel-minLightLevel)/(maxLightLevel-minLightLevel));

        
    }
}
