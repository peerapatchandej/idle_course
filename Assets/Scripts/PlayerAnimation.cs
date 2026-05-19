using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
  public Animator animator;
  public SpriteRenderer spriteRenderer;
  public PlayerController player;

  public float normalAttackDuration = 0.5f;

  private float lockUntilTime;
  private string currentState;

  private void Update()
  {
    if (player.facingDirection.x > 0.01f)
    {
      spriteRenderer.flipX = false;
    }
    else if (player.facingDirection.x < -0.01f)
    {
      spriteRenderer.flipX = true;
    }

    if (Time.time < lockUntilTime)
    {
      return;
    }

    if (player.isMoving == true)
    {
      Play("Walk");
    }
    else
    {
      Play("Idle");
    }
  }

  public void PlayNormalAttack()
  {
    Play("Normal_Atk");
    lockUntilTime = Time.time + normalAttackDuration;
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
