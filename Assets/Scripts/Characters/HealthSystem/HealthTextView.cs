using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HealthTextView : HealthView<TextMeshProUGUI>
{
    protected override void UpdateView(int currentValue)
    {
        Component.text = $"{currentValue}/{Health.MaxValue}";
    }
}