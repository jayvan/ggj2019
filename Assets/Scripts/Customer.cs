using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private Slider patienceSlider;

    private const float PATIENCE_MIN = 10.0f;
    private const float PATIENCE_MAX = 15.0f;
    private float patience;
    private float timeRemaining;

    // Start is called before the first frame update
    void Start()
    {
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

    private void DestroyCustomer()
    {
        Destroy(this);
    }

    public void Serve()
    {
        DestroyCustomer();
    }
}
