using UnityEngine;
using UnityEngine.InputSystem;

public class HealthTest : MonoBehaviour
{
  public Health target;

  void Update()
  {
    Keyboard kb = Keyboard.current;
    if (kb.spaceKey.isPressed)
    {
      target.TakeDamage(1);
    }
  }
}
