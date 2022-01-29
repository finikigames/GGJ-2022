using UnityEngine;
using UnityEngine.UI;

namespace GGJ2022.Source.Scripts.UI.Player
{
    public class HealthBar : MonoBehaviour
    {
        public Slider Slider;

        public void InitializeSlider(float maxHealth)
        {
            Slider.maxValue = maxHealth;
            Slider.value = maxHealth;
        }

        public void SetHealth(float health)
        {
            Slider.value = health;
        }
    }
}