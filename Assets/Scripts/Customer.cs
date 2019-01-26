using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : MonoBehaviour
{
    [SerializeField] private Slider patienceSlider;

    public event System.Action onLeave;

    private const float PATIENCE_MIN = 30.0f;
    private const float PATIENCE_MAX = 45.0f;
    private float patience;
    private float timeRemaining;
    private List<Food.FoodType> demands;

    // Start is called before the first frame update
    void Start()
    {
        patience = Random.Range(PATIENCE_MIN, PATIENCE_MAX);
//        patience = 1.0f;
        timeRemaining = patience;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0) { 
            timeRemaining -= Time.deltaTime;
            patienceSlider.value = (patience - timeRemaining) / patience;
        }
        else
        {
            DestroyCustomer();
        }
    }

  public void SetDemands(List<Food.FoodType> demands) {
    this.demands = demands;
  }

    private void DestroyCustomer()
    {
        if(onLeave != null)
        {
            onLeave();
        }
        Destroy(this.gameObject);
    }

    public bool Serve(Food.FoodType food)
    {
      if (this.demands.Contains(food)) {
        this.demands.Remove(food);
        if (this.demands.Count == 0) {
          DestroyCustomer();
        }

        return true;
      }

      return false;
    }
}
