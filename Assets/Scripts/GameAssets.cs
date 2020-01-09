using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour {
    public static GameAssets i;             // i stands for instance

    private void Awake() {
        i = this;
    }

    public Sprite damage1;
    public Sprite damage2;
    public Sprite damage3;
    public Transform damagePopupPrefab;
    public GameObject player;

}
