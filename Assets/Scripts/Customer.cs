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
    [SerializeField] private GameObject bubbleTea;
    [SerializeField] private GameObject shavedIce;
    [SerializeField] private GameObject orderBox;

    public event System.Action onLeave;

    private const float PATIENCE_MIN = 30.0f;
    private const float PATIENCE_MAX = 45.0f;
    private float patience;
    private float timeRemaining;
    private float relationshipScore;

    private Dictionary<Food.FoodType, GameObject> foodDictionary;
    private List<Food.FoodType> demands;
    private List<GameObject> displayedDemands = new List<GameObject>();

    private void Awake()
    {
        foodDictionary = new Dictionary<Food.FoodType, GameObject>();
        foreach (Food.FoodType food in System.Enum.GetValues(typeof(Food.FoodType)))
        {
            if (food == Food.FoodType.BubbleTea)
            {
                foodDictionary.Add(food, bubbleTea);
            }
            else if (food == Food.FoodType.ShavedIce)
            {
                foodDictionary.Add(food, shavedIce);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        patience = Random.Range(PATIENCE_MIN, PATIENCE_MAX);
        relationshipScore = 2;
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

        foreach (Food.FoodType food in demands) {
            GameObject newFood = (GameObject)Instantiate(foodDictionary[food], orderBox.transform, false);
            displayedDemands.Add(newFood);
        }
    }

    public bool Serve(Food.FoodType food)
    {
        if (this.demands.Contains(food)) {
            int index = this.demands.IndexOf(food);
            this.demands.Remove(food);

            // need way to show it has been served before removing from this list
            this.displayedDemands.RemoveAt(index);

            if (this.demands.Count == 0) {
                UpdateRelationship();
            }

            return true;
        }

        return false;
    }

    public bool HasGoodRelationship()
    {
      return relationshipScore > 0;
    }

    public int GetDemandCount()
    {
        return demands.Count;
    }

    private void UpdateRelationship()
    {
        if (timeRemaining >= patience / 2)
        {
            relationshipScore++;
        }
        else
        {
            relationshipScore--;
        }
    }

    public void DestroyCustomer()
    {
        if (onLeave != null)
        {
            onLeave();
        }
        Destroy(this.gameObject);
    }
}
