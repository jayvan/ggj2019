using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleTeaArea : MonoBehaviour
{

    private const int MAX_TEA = 3;
    private int activeTeaCount = 0;

    // Start is called before the first frame update
    private List<GameObject> bubbleTeaQ;
    [SerializeField] private GameObject bubbleTea;

    void Start()
    {
        bubbleTeaQ = new List<GameObject>();
        while(bubbleTeaQ.Count < MAX_TEA)
        {
            GameObject newTea = (GameObject)Instantiate(bubbleTea, gameObject.transform, false);
            newTea.SetActive(false);
            bubbleTeaQ.Add(newTea);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
        }
    }
}
