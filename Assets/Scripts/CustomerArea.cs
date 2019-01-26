using System.Collections.Generic;
using UnityEngine;

public class CustomerArea : MonoBehaviour {
  [SerializeField] private float minTimeBetweenCustomers;
  [SerializeField] private float maxTimeBetweenCustomers;
  [SerializeField] private int maxCustomers;
  [SerializeField] private GameObject customerPrefab;

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

  public bool Serve() {
    if (this.customers.Count == 0) {
      return false;
    }

    this.customers[0].Serve();
    return true;
  }

  private void SpawnCustomer() {
        Customer newCustomer = Instantiate(this.customerPrefab, this.transform, false).GetComponent<Customer>();
        this.customers.Add(newCustomer);
        newCustomer.onLeave += () =>
        {
            this.customers.Remove(newCustomer);
        };
  }

}
