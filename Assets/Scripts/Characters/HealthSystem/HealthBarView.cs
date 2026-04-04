using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarView : HealthView<Slider>
{
    protected override void UpdateView(int currentValue)
    {
        Component.value = (float)currentValue / Health.MaxValue;
    }
}