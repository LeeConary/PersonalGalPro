using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewTest : MonoBehaviour
{
    [SerializeField]
    RectTransform content;

    [SerializeField]
    int count = 5;

    Image[] images;
    // Start is called before the first frame update
    void Start()
    {
        images = new Image[count];

        content = GetComponent<RectTransform>();
        content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, images.Length * 100f);

        for (int i = 0; i < images.Length; i++)
        {
            Image temp = new GameObject().AddComponent<Image>();
            temp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300f);
            temp.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50f);
            images[i] = temp;
        }

        for (int i = 0; i < images.Length; i++)
        {
            float yIndex = content.rect.height / images.Length * 0.7f;
            images[i].rectTransform.anchoredPosition = new Vector2(0, content.rect.height / images.Length - yIndex*i );
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
