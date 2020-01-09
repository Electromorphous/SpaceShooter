using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    public TextMeshPro textMesh;
    private float vanishTime;
    private float vanishTimeMax;
    private Color textColor;
    private static int sortingOrder;
    private Vector3 speed;
    
    public static DamagePopup Create(Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.damagePopupPrefab, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    public void Setup(int damage)
    {
        textMesh.SetText(damage.ToString());
        textColor = textMesh.color;
        vanishTimeMax = 0.1f;
        vanishTime = vanishTimeMax;
        speed = new Vector3(5f, 5f, 0);
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;
    }

    private void Update()
    {
        transform.position += speed * Time.deltaTime;

        if(vanishTime > vanishTimeMax * 0.5f)
        {
            transform.localScale += Vector3.one * 1f * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * 1f * Time.deltaTime;
        }

        vanishTime -= Time.deltaTime;
        if(vanishTime <= 0)
        {
            float vanishSpeed = 3f;
            textColor.a -= vanishSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a <= 0)
                Destroy(gameObject);

        }
    }
}
