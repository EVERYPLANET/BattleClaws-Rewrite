using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeText : MonoBehaviour
{
    private TextMeshProUGUI textObject;
    private Image buttonPromptImage;
    [SerializeField]  private float speed;

    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
        buttonPromptImage = GetComponent<Image>();

        if(buttonPromptImage != null)
        {
            StartCoroutine(flashImage());
        }
        
       
    }

    private void Update()
    {
        if (textObject != null)
        {
            FadeInText();
        }
    }

    public void FadeInText()
    {
        {
            textObject.alpha = Mathf.Sin(Time.time * speed) * 0.5f + 0.5f;
        }
    }

    public IEnumerator flashImage()
    {
        yield return new WaitForSeconds(0.5f);

        buttonPromptImage.color -= new Color(0f, 0f, 0f, 1f);

        yield return new WaitForSeconds(0.5f);

        buttonPromptImage.color += new Color(0f, 0f, 0f, 1f);

        StartCoroutine(flashImage());
    }
}