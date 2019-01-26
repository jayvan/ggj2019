using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private List<Food.FoodType> demands;
    private Dictionary<Food.FoodType, GameObject> foodDictionary;

    // Start is called before the first frame update
    void Start()
    {
        foodDictionary = new Dictionary<Food.FoodType, GameObject>();
        foreach (Food.FoodType food in System.Enum.GetValues(typeof(Food.FoodType)))
        {
            if(food == Food.FoodType.BubbleTea)
            {
                foodDictionary.Add(food, bubbleTea);
            } 
            else if(food == Food.FoodType.ShavedIce)
            {
                foodDictionary.Add(food, shavedIce);
            }
        }
        patience = Random.Range(PATIENCE_MIN, PATIENCE_MAX);
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
            GameObject newFood = (GameObject)Instantiate(foodDictionary[food], gameObject.transform, false);
        }
    }

    private void DestroyCustomer()
    {
        if(onLeave != null)
        {
            onLeave();
        }
        Destroy(this.gameObject);
    }

    public void Serve()
    {
        DestroyCustomer();
    }
}
