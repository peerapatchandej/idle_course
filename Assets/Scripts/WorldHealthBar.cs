using UnityEngine;
using UnityEngine.UI;

public class WorldHealthBar : MonoBehaviour
{
  public Health target;
  public Image fillImage;

  private void LateUpdate()
  {
    float ratio;

    if (target.maxHealth <= 0)
    {
      ratio = 0;
    }
    else
    {
      ratio = target.currentHealth / target.maxHealth;
    }

    ratio = Mathf.Clamp01(ratio);
    fillImage.fillAmount = ratio;
  }
}
