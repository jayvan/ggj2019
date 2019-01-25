using UnityEngine;
using UnityEngine.UI;

public class BubbleTeaAppliance : MonoBehaviour {
  [SerializeField] private Button bubbleTeaMachine;
  [SerializeField] private Slider progressSlider;
  [SerializeField] private BubbleTeaArea teaArea;
  [SerializeField] private float preparationTime;
  private bool preparing;
  private float timeRemaining;

  void Start() {
    this.bubbleTeaMachine.onClick.AddListener(StartTea);
  }

  void Update() {
    if (preparing) {
      this.timeRemaining -= Time.deltaTime;
      this.progressSlider.value = (this.preparationTime - this.timeRemaining) / this.preparationTime;

      if (this.timeRemaining < 0) {
        this.preparing = false;
        this.teaArea.AddTea();
        this.progressSlider.value = 0;
      }
    }
  }

  void StartTea() {
    if (this.teaArea.IsFull || this.preparing) {
      return;
    }

    this.preparing = true;
    this.timeRemaining = this.preparationTime;
  }
}
