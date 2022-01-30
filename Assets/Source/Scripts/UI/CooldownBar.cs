using UnityEngine;
using UnityEngine.UI;

namespace GGJ2022.Source.Scripts.UI
{
    public class CooldownBar : MonoBehaviour
    {
        public Slider Slider;

        public void InitializeSlider(float maxHealth)
        {
            Slider.maxValue = maxHealth;
            Slider.value = maxHealth;
        }

        public void Set(float health)
        {
            Slider.value = health;
        }
    }
}