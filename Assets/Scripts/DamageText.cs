using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float moveSpeed;
    public float alphaSpeed;
    public float destroyTime;

    TextMeshPro text;
    Color alpha;
    public int damage;

    void Start()
    {
      //  moveSpeed = 1.0f;
      //  alphaSpeed = 2.0f;
      //  destroyTime = 1.0f;

        text = GetComponent<TextMeshPro>();

        if (text == null)
        {
            Debug.LogError("TextMeshPro component not found on this GameObject.");
        }
        else
        {
            alpha = text.color;
            text.text = damage.ToString();
            Invoke("DestroyObject", destroyTime);
        }
    }

   
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0)); // 텍스트 위치

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed); // 텍스트 알파값
        text.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
