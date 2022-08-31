using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HipsModelChanger : MonoBehaviour
{
    public List<GameObject> HipsModels;

    private void Awake()
    {
        GetAllHipsModels();
    }

    private void GetAllHipsModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            HipsModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnequipAllHipsModels()
    {
        foreach (GameObject hipModel in HipsModels)
        {
            hipModel.SetActive(false);
        }
    }

    public void EquipHipModelByName(string hipName)
    {
        for (int i = 0; i < HipsModels.Count; i++)
        {
            if (HipsModels[i].name == hipName)
            {
                HipsModels[i].SetActive(true);
            }
        }
    }
}
