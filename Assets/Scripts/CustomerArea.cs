using System.Collections.Generic;
using UnityEngine;

public class CustomerArea : MonoBehaviour {
  [SerializeField] private float minTimeBetweenCustomers;
  [SerializeField] private float maxTimeBetweenCustomers;
  [SerializeField] private int maxCustomers;
  [SerializeField] private GameObject customerPrefab;
  [SerializeField] private Menu menu;

  private float timeToNextCustomer = 1f;
  List<Customer> customers = new List<Customer>();

  void Update() {
    if (this.customers.Count < this.maxCustomers) {
      this.timeToNextCustomer -= Time.deltaTime;
    }

    if (this.timeToNextCustomer < 0) {
      this.SpawnCustomer();
      this.timeToNextCustomer += Random.Range(this.minTimeBetweenCustomers, this.maxTimeBetweenCustomers);
    }
  }

  public bool Serve(Food.FoodType food) {
    foreach (Customer customer in this.customers) {
      if (customer.Serve(food)) {
        if (customer.GetDemandCount() == 0) {

          if(!customer.HasGoodRelationship()) {
            customer.DestroyCustomer();
          } else {
            // take this out and replace with something else to store customer
            customer.DestroyCustomer();
          }
        }
        return true;
      }
    }

    return false;
  }

    private void SpawnCustomer() {
        Customer newCustomer = Instantiate(this.customerPrefab, this.transform, false).GetComponent<Customer>();
        newCustomer.SetDemands(this.menu.Random(3));

        this.customers.Add(newCustomer);
        newCustomer.onLeave += () =>
        {
            this.customers.Remove(newCustomer);
        };
  }
}
