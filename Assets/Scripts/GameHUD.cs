using UnityEngine;
using TMPro;

public class GameHUD : MonoBehaviour
{
  public AutoCombat autoCombat;
  public SkillController skillController;

  public TextMeshProUGUI autoLabel;
  public TextMeshProUGUI aoeCdText;

  private void Update()
  {
    UpdateAutoLabel();
    UpdateSkillCooldowns();
  }

  private void UpdateAutoLabel()
  {
    if (autoCombat.isAutoEnabled)
    {
      autoLabel.text = "AUTO: ON";
    }
    else
    {
      autoLabel.text = "AUTO: OFF";
    }
  }

  private void UpdateSkillCooldowns()
  {
    AOESkill aoe = skillController.aoeSkill;
    SetCdText(aoeCdText, aoe.IsReady(), aoe.CooldownRemaining());
  }

  private void SetCdText(TextMeshProUGUI label, bool isReady, float remaining)
  {
    if (isReady)
    {
      label.text = "";
    }
    else
    {
      label.text = remaining.ToString("0.0");
    }
  }
}
