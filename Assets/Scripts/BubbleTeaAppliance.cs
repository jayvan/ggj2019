using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTeaAppliance : MonoBehaviour {

    [SerializeField] private Button bubbleTeaMachine;
    [SerializeField] private BubbleTeaArea teaArea;

    void Start() {
      this.bubbleTeaMachine.onClick.AddListener(StartTea);
      bool foo = this.teaArea.IsFull;
      this.teaArea.AddTea();

    }

    void Update() {

    }

  void StartTea() {
    // check
  }
}
