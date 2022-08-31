using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightLegModelChanger : MonoBehaviour
{
    public List<GameObject> RightLegModels;

    private void Awake()
    {
        GetAllRightLegModels();
    }

    private void GetAllRightLegModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            RightLegModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnequipAllRightLegModels()
    {
        foreach (GameObject rightLegModel in RightLegModels)
        {
            rightLegModel.SetActive(false);
        }
    }

    public void EquipRightLegModelByName(string rightLegName)
    {
        for (int i = 0; i < RightLegModels.Count; i++)
        {
            if (RightLegModels[i].name == rightLegName)
            {
                RightLegModels[i].SetActive(true);
            }
        }
    }
}
