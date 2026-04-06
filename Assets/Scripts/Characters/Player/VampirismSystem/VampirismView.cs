using UnityEngine;
using UnityEngine.UI;

public class VampirismView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _radiusSprite;
    [SerializeField] private Slider _slider;

    public void SetActive(bool value)
    {
        if (_radiusSprite != null)
            _radiusSprite.enabled = value;
    }
    
    public void SetAmount(float value)
    {
        if (_slider != null)
            _slider.value = 1f - value;
    }
}
