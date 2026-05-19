using UnityEngine;
using UnityEngine.InputSystem;

public class SkillTest : MonoBehaviour
{
  public AOESkill skill;

  private void Update()
  {
    Keyboard kb = Keyboard.current;
    if (kb.spaceKey.isPressed == true)
    {
      skill.TryCast();
    }
  }
}
