using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    public static GameAssets i;             // i stands for instance

    private void Awake() {
        i = this;
    }

    public int mapSize;
    public float laserSpeed;
    public Sprite damage1, damage2, damage3;
    public Sprite shield1, shield2, shield3;
    public Transform damagePopupPrefab;
    public GameObject player;

    public bool IsPowerUp(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("bluePill") || hitInfo.CompareTag("greenPill") || hitInfo.CompareTag("yellowPill") || hitInfo.CompareTag("ShieldPowerUp") || hitInfo.CompareTag("Adrenaline"))
            return true;
        return false;
    }

    int HexToDec(string hex)
    {
        return System.Convert.ToInt32(hex, 16);
    }
    string DecToHex(int dec)
    {
        return dec.ToString("X2");
    }
    string FloatNormalizedToHex(float value)
    {
        return DecToHex(Mathf.RoundToInt(value * 255f));
    }
    float HexToFloatNormalized(string hex)
    {
        return HexToDec(hex) / 255f;
    }
    public Color GetColorFromHex(string hex)
    {
        float red = HexToFloatNormalized(hex.Substring(0, 2));
        float green = HexToFloatNormalized(hex.Substring(2, 2));
        float blue = HexToFloatNormalized(hex.Substring(4, 2));
        float alpha = 1f;
        if(hex.Length >= 8)
            alpha = HexToFloatNormalized(hex.Substring(6, 2));
        
        return new Color(red, green, blue, alpha);
    }
    public string GetHexFromColor(Color color, bool useAlpha)
    {
        string red = FloatNormalizedToHex(color.r);
        string green = FloatNormalizedToHex(color.g);
        string blue = FloatNormalizedToHex(color.b);
        if(!useAlpha)
            return red + green + blue;
        else
        {
            string alpha = FloatNormalizedToHex(color.a);
            return red + green + blue + alpha;
        }
    }

    public IEnumerator ChangeColor(string hex, GameObject colorChanging, GameObject toDestroy)         //changes the color of player from the given color to white smoothly
    {
        float r, g, b, a;
        r = HexToFloatNormalized(hex.Substring(0, 2));
        g = HexToFloatNormalized(hex.Substring(2, 2));
        b = HexToFloatNormalized(hex.Substring(4, 2));
        a = 1f;
        if (hex.Length >= 8)
            a = HexToFloatNormalized(hex.Substring(6, 2));

        while(r < 1 || g < 1 || b < 1 || a < 1)
        {
            
            colorChanging.GetComponent<SpriteRenderer>().color = GetColorFromHex(FloatNormalizedToHex(r) + FloatNormalizedToHex(g) + FloatNormalizedToHex(b) + FloatNormalizedToHex(a));

            if (r < 1)
                r += Time.deltaTime * 3;
            else
                r = 1;

            if (g < 1)
                g += Time.deltaTime * 3;
            else
                g = 1;

            if (b < 1)
                b += Time.deltaTime * 3;
            else
                b = 1;

            if (a < 1)
                a += Time.deltaTime * 3;
            else
                a = 1;

            yield return null;
        }
        Destroy(toDestroy);
    }
}
