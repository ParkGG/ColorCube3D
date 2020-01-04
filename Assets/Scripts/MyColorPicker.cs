using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyColorPicker : MonoBehaviour
{

    public RawImage colorSpace;

    public Slider[] rgbSlider;
    public InputField[] rgbInputfiled;

    private Color32 originColor;
    private Color32 changeColor;
    private Material targetMaterial;


    public void NotifyColor(GameObject select)
    {

        targetMaterial = select.GetComponent<Renderer>().sharedMaterial;

        originColor = targetMaterial.color;

        rgbSlider[0].value = originColor.r;
        rgbSlider[1].value = originColor.g;
        rgbSlider[2].value = originColor.b;
        rgbInputfiled[0].text = originColor.r.ToString();
        rgbInputfiled[1].text = originColor.g.ToString();
        rgbInputfiled[2].text = originColor.b.ToString();


        rgbSlider[0].onValueChanged.AddListener(delegate { UpdateSlidertoText(0); });
        rgbInputfiled[0].onValueChanged.AddListener(delegate { UpdateTexttoSlider(0); });
        rgbSlider[1].onValueChanged.AddListener(delegate { UpdateSlidertoText(1); });
        rgbInputfiled[1].onValueChanged.AddListener(delegate { UpdateTexttoSlider(1); });
        rgbSlider[2].onValueChanged.AddListener(delegate { UpdateSlidertoText(2); });
        rgbInputfiled[2].onValueChanged.AddListener(delegate { UpdateTexttoSlider(2); });
    }
   

    private void Update()
    {
        Texture2D texture = (Texture2D)colorSpace.mainTexture;
        Rect rect = colorSpace.rectTransform.rect;

        Color32 color;

        if (Input.GetMouseButtonDown(0) && rect.Contains(Input.mousePosition))
        {
            Vector2 mousePos = Input.mousePosition;
            color = (Color32)texture.GetPixel((int)mousePos.x, (int)mousePos.y);
        }

    }

    void UpdateSlidertoText(int ix)
    {
        if(rgbInputfiled[ix].text != rgbSlider[ix].value.ToString())
            rgbInputfiled[ix].text = rgbSlider[ix].value.ToString();

        changeColor.r = byte.Parse(rgbSlider[0].value.ToString());
        changeColor.g = byte.Parse(rgbSlider[1].value.ToString());
        changeColor.b = byte.Parse(rgbSlider[2].value.ToString());
        changeColor.a = 255;
        UpdateColor();
    }
    void UpdateTexttoSlider(int ix)
    {
        if(rgbSlider[ix].value != float.Parse(rgbInputfiled[ix].text))
            rgbSlider[ix].value = float.Parse(rgbInputfiled[ix].text);

        changeColor.r = byte.Parse(rgbInputfiled[0].text);
        changeColor.g = byte.Parse(rgbInputfiled[1].text);
        changeColor.b = byte.Parse(rgbInputfiled[2].text);
        changeColor.a = 255;
        UpdateColor();
    }

    void UpdateColor()
    {
        targetMaterial.SetColor("_MKGlowColor", changeColor);
        targetMaterial.SetColor("_Color", changeColor);
    }

    public void ApplyColor()
    {
        originColor = changeColor;
        targetMaterial.SetColor("_MKGlowColor", originColor);
        targetMaterial.SetColor("_Color", originColor);
        

    }

    private void OnDisable()
    {
        targetMaterial.SetColor("_MKGlowColor", originColor);
        targetMaterial.SetColor("_Color", originColor);
    }

}
