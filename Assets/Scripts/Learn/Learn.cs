
using System;
using UnityEngine;
using System.Collections.Generic;

public class Learn : MonoBehaviour
{
  //public Player player1;
  //public Player player2;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    //Player player1 = new Player();
    //player1.CreatePlayer("Hero", 100, 20, 10, "Male");
    //Debug.Log("Player Name: " + player1.playerName);
    //Debug.Log("Player Health: " + player1.health);
    //Debug.Log("Player Attack Power: " + player1.attackPower);
    //Debug.Log("Player Defense: " + player1.defense);

    //Debug.Log("================================");

    //Player player2 = new Player();
    //player2.CreatePlayer("Heroine", 80, 10, 5, "Female");
    //Debug.Log("Player Name: " + player2.playerName);
    //Debug.Log("Player Health: " + player2.health);
    //Debug.Log("Player Attack Power: " + player2.attackPower);
    //Debug.Log("Player Defense: " + player2.defense);

    //Debug.Log("================================");

    //player1.Attack(player2);
    //Debug.Log("Player2 Health after attack: " + player2.health);

    //Player player1 = GetComponent<Player>();
    //Debug.Log("Player Name: " + player1.playerName);
    //Debug.Log("Player Health: " + player1.health);
    //Debug.Log("Player Attack Power: " + player1.attackPower);
    //Debug.Log("Player Defense: " + player1.defense);

    //player1.CreatePlayer("Heroine", 80, 10, 5, "Female");

    //Debug.Log("Player Name: " + player1.playerName);
    //Debug.Log("Player Health: " + player1.health);
    //Debug.Log("Player Attack Power: " + player1.attackPower);
    //Debug.Log("Player Defense: " + player1.defense);

    //player1.Attack(player2);

    int[] numbers = new int[7];   //index 0 1 2 3 4 5 6
    numbers[0] = 10;
    numbers[1] = 20;
    numbers[2] = 30;
    numbers[3] = 40;
    numbers[4] = 50;
    numbers[5] = 60;
    numbers[6] = 70;

    List<int> numberList = new List<int>();
    numberList.Add(10);   //10 -> 0
    numberList.Add(20);   //20 -> 1

    for (int index = 0; index < numberList.Count; index++)
    {
      Debug.Log(index + " " + numberList[index]);   //1 2 3
    }

    //int index = 0;
    //while (index < 7) //0
    //{
    //  Debug.Log("Number at index " + index + ": " + numbers[index]);  // 0 10 | 1 20
    //  index += 1; //0 > 1
    //}

    //foreach (int number in numbers)
    //{
    //  Debug.Log("Number : " + number);
    //}

    //for (int index = 0; index < numbers.Length; index++)  // 1 <= 3 | 1 > 2, 2 <= 3 | 2 > 3, 3 <= 3 | 3 > 4, 4 <= 3
    //{
    //  Debug.Log(index + " " + numbers[index]);   //1 2 3
    //}
  }

  // Update is called once per frame
  void Update()
  {

  }

  void AddNumber1()
  {
    int number1 = 20;
    int number2 = 30;
    int sum = number1 + number2;
    Debug.Log("Sum is: " + sum);
  }

  void AddNumber2(int number1, int number2)
  {
    int sum = number1 + number2;
    Debug.Log("Sum is: " + sum);
  }

  int AddNumber3(int number1, int number2)
  {
    return number1 + number2;
  }

  string AddNumber4(int number1, int number2)
  {
    int sum = number1 + number2;
    return "Sum is: " + sum;
  }
}
