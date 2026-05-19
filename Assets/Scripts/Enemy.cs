using UnityEngine;

public class Enemy : MonoBehaviour
{
  public float moveSpeed = 2f;
  public float attackRange = 0.9f;
  public float stopBuffer = 0.25f;
  public float attackDamage = 5f;
  public float attackInterval = 1f;
  public Vector2 facingDirection = Vector2.right;
  public bool isMoving;

  private Rigidbody2D rb;
  private Health health;
  private EnemyAnimation enemyAnimation;

  private Transform target;

  private float attackTimer;  //0f by default
  private bool isInAttackRange;
  private bool isDying;   //false by default
  //private float destroyAtTime;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    health = GetComponent<Health>();
    enemyAnimation = GetComponent<EnemyAnimation>();
    GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    target = playerObj.transform;
  }

  private void Update()
  {
    if (health.isDead && !isDying)  //isDying = false -> true
    {
      isDying = true;
      enemyAnimation.PlayDead();    //time = 10 sec
      Destroy(gameObject, enemyAnimation.deadDuration);
      //destroyAtTime = Time.time + enemyAnimation.deadDuration;    //10 sec + 0.5 sec
    }

    //if (isDying && Time.time >= destroyAtTime)
    //{
    //  Destroy(gameObject);
    //}
  }

  private void FixedUpdate()
  {
    if (isDying || health.isDead)
    {
      isMoving = false;
      return;
    }

    Vector2 toTarget = (Vector2)target.position - rb.position;
    float distance = toTarget.magnitude;

    if (isInAttackRange)
    {
      if (distance > attackRange + stopBuffer)
      {
        isInAttackRange = false;
      }
    }
    else
    {
      if (distance <= attackRange)
      {
        isInAttackRange = true;
      }
    }

    if (isInAttackRange)
    {
      rb.linearVelocity = Vector2.zero;   //stop moving immediately
      isMoving = false;

      attackTimer = attackTimer - Time.fixedDeltaTime;
      if (attackTimer <= 0f)
      {
        attackTimer = attackInterval;   //reset attack timer to 1 sec
        TryAttack(toTarget);
      }
    }
    else
    {
      Vector2 dir = toTarget.normalized;
      rb.MovePosition(rb.position + dir * (moveSpeed * Time.fixedDeltaTime));
      isMoving = true;

      if (dir.sqrMagnitude > 0.01f)
      {
        facingDirection = dir;
      }
    }
  }

  private void TryAttack(Vector2 toTarget)
  {
    Vector2 dir = toTarget.normalized;
    if (dir.sqrMagnitude > 0.01f)
    {
      facingDirection = dir;
    }

    enemyAnimation.PlayAttack();

    Health playerHealth = target.GetComponent<Health>();
    playerHealth.TakeDamage(attackDamage);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, attackRange);

    Gizmos.color = Color.softYellow;
    Gizmos.DrawWireSphere(transform.position, attackRange + stopBuffer);
  }
}
