using UnityEngine;

public class Player : MonoBehaviour
{
  public string playerName;
  public int health;
  public int attackPower;
  public int defense;
  public string gender;

  public void CreatePlayer(string name, int hp, int atk, int def, string gender)
  {
    playerName = name;
    health = hp;
    attackPower = atk;
    defense = def;
    this.gender = gender;
  }

  public void Attack(Player target)
  {
    int damage = attackPower - target.defense;
    if (damage > 0)
    {
      target.health = target.health - damage;
      Debug.Log(playerName + " attacks " + target.playerName + " for " + damage + " damage!");
    }
    else
    {
      Debug.Log(playerName + "'s attack was too weak to harm " + target.playerName);
    }
  }
}

//public class Player
//{
//  public string playerName;
//  public int health;
//  public int attackPower;
//  public int defense;
//  private string gender;

//  public void CreatePlayer(string name, int hp, int atk, int def, string gender)
//  {
//    playerName = name;
//    health = hp;
//    attackPower = atk;
//    defense = def;
//    this.gender = gender;
//  }

//  public void Attack(Player target)
//  {
//    int damage = attackPower - target.defense;  // 20 - 5 = 15
//    if (damage > 0)
//    {
//      target.health = target.health - damage;   //80 - 15 = 65
//      Debug.Log(playerName + " attacks " + target.playerName + " for " + damage + " damage!");
//    }
//    else
//    {
//      Debug.Log(playerName + "'s attack was too weak to harm " + target.playerName);
//    }
//  }
//}