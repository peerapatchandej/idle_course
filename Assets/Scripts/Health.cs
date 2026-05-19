using UnityEngine;

public class Health : MonoBehaviour
{
  public float maxHealth = 100f;
  public float currentHealth;
  public bool isDead;   // = false by default

  void Start()
  {
    currentHealth = maxHealth;
  }

  public void TakeDamage(float damage)
  {
    if (isDead)
    {
      return;
    }

    currentHealth -= damage;    //currentHealth = currentHealth - damage;

    if (currentHealth <= 0f)
    {
      currentHealth = 0f;
      isDead = true;
    }
  }
}
