using UnityEngine;

public class AutoCombat : MonoBehaviour
{
  public bool isAutoEnabled = true;
  public float scanRadius = 8f;
  public LayerMask enemyMask;
  public float decisionInterval = 0.1f;
  public float stopDistance = 1.2f;

  private SkillController skills;
  private PlayerController player;
  private float decisionTimer;  //0

  private void Start()
  {
    skills = GetComponent<SkillController>();
    player = GetComponent<PlayerController>();
  }

  private void Update()
  {
    if (!player.HasManualInput())
    {
      UpdateAutoMove();
    }

    if (!isAutoEnabled || player.HasManualInput())
    {
      return;
    }

    decisionTimer = decisionTimer - Time.deltaTime; //0.1f - 0.016f -> 0.084f
    if (decisionTimer > 0f)
    {
      return;
    }

    decisionTimer = decisionInterval; //0.1f
    RunAutoDecision();
  }

  private void UpdateAutoMove()
  {
    Vector2 toTarget = GetNearestEnemyPosition();
    if (toTarget == Vector2.zero)   //ไม่มีศัตรูในระยะสแกนเลย
    {
      return;
    }

    if (toTarget.magnitude <= stopDistance)
    {
      return;
    }

    player.RequestAutoMove(toTarget.normalized); //-1 0 1
  }

  private void RunAutoDecision()
  {
    Vector2 toTarget = GetNearestEnemyPosition();
    if (toTarget == Vector2.zero)
    {
      return;
    }

    if (skills.aoeSkill.IsReadyAuto())
    {
      skills.TryCastAOE(toTarget.normalized); //-1 0 1
      return;
    }
  }

  public Vector2 GetNearestEnemyPosition()
  {
    Transform nearest = FindNearestEnemy();
    if (nearest == null)
    {
      return Vector2.zero;  //ไม่มีศัตรูในระยะสแกนเลย
    }
    return (Vector2)nearest.position - (Vector2)transform.position;   //ตำแหน่งศัตรู - ตำแหน่งตัวเอง = เวกเตอร์ชี้ไปหาศัตรู
  }

  private Transform FindNearestEnemy()
  {
    Vector2 self = transform.position;
    Collider2D[] hits = Physics2D.OverlapCircleAll(self, scanRadius, enemyMask);

    Transform best = null;
    float bestDist = float.MaxValue;

    foreach (Collider2D col in hits)
    {
      Vector2 pos = col.transform.position;

      if (!ArenaBounds.Contains(pos))
      {
        continue; //Next loop, ไม่สนใจศัตรูที่อยู่นอกจอ
      }

      float distSqr = (pos - self).sqrMagnitude;
      if (distSqr < bestDist)
      {
        bestDist = distSqr;
        best = col.transform;
      }
    }

    return best;
  }

  public void ToggleAuto()
  {
    isAutoEnabled = !isAutoEnabled;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, scanRadius);

    Gizmos.color = Color.white;
    Gizmos.DrawWireSphere(transform.position, stopDistance);
  }
}
