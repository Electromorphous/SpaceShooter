using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    public static GameAssets i;             // i stands for instance

    private void Awake() {
        i = this;
    }

    public int mapSize;
    public Sprite damage1;
    public Sprite damage2;
    public Sprite damage3;
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
        return new Color(red, green, blue);
    }
}
