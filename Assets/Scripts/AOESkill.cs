using UnityEngine;

public class AOESkill : MonoBehaviour
{
  public float damage = 15f;
  public float cooldown = 1.5f;
  public float radius = 3f;
  public LayerMask enemyMask;
  public GameObject sunStrikePrefab;
  public float effectDuration = 0.5f;

  private float cooldownTimer;

  private void Update()
  {
    if (cooldownTimer > 0f) //1.5f
    {
      cooldownTimer = cooldownTimer - Time.deltaTime; //0 / -0.1
    }
  }

  public bool IsReady()
  {
    return cooldownTimer <= 0f;
  }

  public bool IsReadyAuto()
  {
    Vector2 origin = transform.position;
    Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyMask);

    return cooldownTimer <= 0f && hits.Length > 0;
  }

  public float CooldownRemaining()
  {
    return Mathf.Max(0f, cooldownTimer); //0 , -0.1
  }

  public bool TryCast()
  {
    if (!IsReady())
    {
      return false;
    }

    Vector2 origin = transform.position;
    Collider2D[] hits = Physics2D.OverlapCircleAll(origin, radius, enemyMask);

    foreach (Collider2D col in hits)
    {
      Health h = col.GetComponent<Health>();
      h.TakeDamage(damage);
    }

    GameObject fx = Instantiate(sunStrikePrefab, origin, Quaternion.identity);
    Destroy(fx, effectDuration);

    cooldownTimer = cooldown;

    return true;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
