using UnityEngine;

public class SkillController : MonoBehaviour
{
  public AOESkill aoeSkill;

  private float skillBusyUntilTime;

  //
  private PlayerController player;
  private AutoCombat autoCombat;

  //
  private void Start()
  {
    player = GetComponent<PlayerController>();
    autoCombat = GetComponent<AutoCombat>();
  }

  public void UseAOEWithButton()
  {
    //
    if (IsAutoOn())
    {
      return;
    }
    TryCastAOE(GetAimDirection()); //
  }

  public void TryCastAOE(Vector2 direction)
  {
    if (!aoeSkill.TryCast())
    {
      return;
    }
    AfterCast(direction /**/, aoeSkill.effectDuration);
  }

  private void AfterCast(Vector2 direction /**/, float castLockoutTime)
  {
    //
    if (direction.sqrMagnitude > 0.01f)
    {
      player.facingDirection = direction.normalized;
    }

    skillBusyUntilTime = Time.time + castLockoutTime;
  }

  public bool IsBusyCastingSkill()
  {
    return Time.time < skillBusyUntilTime;
  }

  //
  public bool IsAutoOn()
  {
    if (!autoCombat.isAutoEnabled || player.HasManualInput())
    {
      return false;
    }
    return true;
  }

  //
  public Vector2 GetAimDirection()
  {
    Vector2 dir = autoCombat.GetNearestEnemyPosition().normalized;

    if (dir != Vector2.zero)
    {
      return dir;
    }

    return player.facingDirection;
  }
}
