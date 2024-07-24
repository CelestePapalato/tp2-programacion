using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Vida vidaComponent;
    Slider healthbarSlider;

    private void Awake()
    {
        healthbarSlider = GetComponent<Slider>();
        if (!healthbarSlider || !vidaComponent) { gameObject.SetActive(false); }
    }

    private void OnEnable()
    {
        if(vidaComponent != null)
        {
            vidaComponent.OnHealthUpdate += UpdateSlider;
        }
    }

    private void OnDisable()
    {
        if (vidaComponent != null)
        {
            vidaComponent.OnHealthUpdate -= UpdateSlider;
        }
    }

    public void UpdateSlider(int value, int maxValue)
    {
        float slider_value = (float) value / maxValue;
        healthbarSlider.value = slider_value;
    }
}
