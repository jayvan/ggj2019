using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleTeaArea : MonoBehaviour
{
    private const int MAX_TEA = 3;
    private int activeTeaCount = 0;

    // Start is called before the first frame update
    private List<GameObject> bubbleTeaQ;
    [SerializeField] private GameObject bubbleTea;
    [SerializeField] private Button button;
    [SerializeField] private CustomerArea customerArea;

    void Start()
    {
      this.button.onClick.AddListener(this.RemoveTea);
        bubbleTeaQ = new List<GameObject>();
        while(bubbleTeaQ.Count < MAX_TEA)
        {
            GameObject newTea = (GameObject)Instantiate(bubbleTea, gameObject.transform, false);
            newTea.SetActive(false);
            bubbleTeaQ.Add(newTea);
        }
    }

    public bool IsFull
    {
        get
        {   
            return activeTeaCount == MAX_TEA;
        }
    }

    public void AddTea() 
    {
        if(activeTeaCount < MAX_TEA)
        {
            bubbleTeaQ[activeTeaCount++].SetActive(true);
        }
        else
        {
            throw new System.Exception("Too many tea's");
        }
         
    }

    public void RemoveTea()
    {
        if(activeTeaCount > 0)
        {
            bubbleTeaQ[--activeTeaCount].SetActive(false);
            this.customerArea.Serve();
        }
    }
}
