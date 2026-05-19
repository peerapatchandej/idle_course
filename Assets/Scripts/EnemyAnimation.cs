using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
  public Animator animator;
  public SpriteRenderer spriteRenderer;
  public Enemy enemy;

  public float attackDuration = 0.5f;
  public float deadDuration = 0.5f;   //10 frame / 20 frame per sec = 0.5 sec

  private float lockUntilTime;
  private string currentState;
  private bool isDeadPlaying;   //false

  public void PlayAttack()
  {
    if (isDeadPlaying)
    {
      return;
    }
    Play("Attack");
    lockUntilTime = Time.time + attackDuration;
  }

  public void PlayDead()
  {
    if (isDeadPlaying)
    {
      return;
    }
    isDeadPlaying = true;
    Play("Dead");
  }

  private void Update()
  {
    if (isDeadPlaying)
    {
      return;
    }

    if (enemy.facingDirection.x > 0.01f)
    {
      spriteRenderer.flipX = false;
    }
    else if (enemy.facingDirection.x < -0.01f)
    {
      spriteRenderer.flipX = true;
    }

    if (Time.time < lockUntilTime)
    {
      return;
    }

    if (enemy.isMoving)
    {
      Play("Walk");
    }
    else
    {
      Play("Idle");
    }
  }

  private void Play(string stateName)
  {
    if (currentState == stateName)
    {
      return;
    }
    animator.Play(stateName);
    currentState = stateName;
  }
}
