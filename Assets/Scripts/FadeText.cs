using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeText : MonoBehaviour
{
    private TextMeshProUGUI textObject;
    [SerializeField]  private float speed;

    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        FadeInText();
    }

    public void FadeInText()
    {
        {
            textObject.alpha = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        }
    }
}