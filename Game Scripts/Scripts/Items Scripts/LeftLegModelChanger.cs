using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegModelChanger : MonoBehaviour
{
    public List<GameObject> LeftLegModels;

    private void Awake()
    {
        GetAllLeftLegModels();
    }

    private void GetAllLeftLegModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            LeftLegModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnequipAllLeftLegModels()
    {
        foreach (GameObject leftLegModel in LeftLegModels)
        {
            leftLegModel.SetActive(false);
        }
    }

    public void EquipLeftLegModelByName(string leftLegName)
    {
        for (int i = 0; i < LeftLegModels.Count; i++)
        {
            if (LeftLegModels[i].name == leftLegName)
            {
                LeftLegModels[i].SetActive(true);
            }
        }
    }
}
