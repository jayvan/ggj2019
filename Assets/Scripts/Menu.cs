using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

[Serializable]
public struct MenuItem {
  public Food.FoodType food;
  public int weight;
}

[CreateAssetMenu(fileName = "Menu", menuName = "Data/Menu", order = 1)]
public class Menu : ScriptableObject {
  public MenuItem[] items;
  private Random random = new Random();

  private int TotalWeight {
    get {
      return this.items.Sum(item => item.weight);
    }
  }

  public List<Food.FoodType> Random(int maxFoodCount) {
    int demandCount = this.random.Next(1, maxFoodCount + 1);
    List<Food.FoodType> demands = new List<Food.FoodType>(demandCount);

    for (int i = 0; i < demandCount; i++) {
      demands.Add(this.PickOne());
    }

    return demands;
  }

  private Food.FoodType PickOne() {
      int roll = this.random.Next(TotalWeight);

      foreach (MenuItem menuItem in this.items) {
        if (roll < menuItem.weight) {
          return menuItem.food;
        }

        roll -= menuItem.weight;
      }

    return this.items[this.items.Length - 1].food;
  }
}
