using UnityEngine;

public class BasicAttack : MonoBehaviour
{
  public float attackRange = 0.8f;
  public float attackCooldown = 0.6f;
  public float damage = 5f;
  public LayerMask enemyMask;
  private float cooldownTimer;

  private PlayerAnimation playerAnimation;
  private PlayerController playerController;
  private SkillController skill;

  private void Start()
  {
    playerAnimation = GetComponent<PlayerAnimation>();
    playerController = GetComponent<PlayerController>();
    skill = GetComponent<SkillController>();
  }

  private void FixedUpdate()
  {
    if (cooldownTimer > 0f)
    {
      cooldownTimer = cooldownTimer - Time.fixedDeltaTime;
      return;
    }

    if (skill.IsBusyCastingSkill() || playerController.HasManualInput())  //
    {
      return;
    }

    Vector2 self = transform.position;
    Collider2D[] hits = Physics2D.OverlapCircleAll(self, attackRange, enemyMask);

    Transform best = null;
    float bestDist = float.MaxValue;
    foreach (Collider2D col in hits)
    {
      float distSqr = ((Vector2)col.transform.position - self).sqrMagnitude;  //0.3
      if (distSqr < bestDist) //0.3 < 0.5
      {
        bestDist = distSqr; //bestDist = 0.3
        best = col.transform; //best = enemy_3
      }
    }

    if (best == null)
    {
      return;
    }

    if (playerController != null)
    {
      Vector2 toEnemy = ((Vector2)best.position - self).normalized; //-1 0 1
      if (toEnemy.sqrMagnitude > 0.01f)
      {
        playerController.facingDirection = toEnemy;
      }
    }

    Health hp = best.GetComponent<Health>();
    hp.TakeDamage(damage);

    playerAnimation.PlayNormalAttack();

    cooldownTimer = attackCooldown;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, attackRange);
  }
}
