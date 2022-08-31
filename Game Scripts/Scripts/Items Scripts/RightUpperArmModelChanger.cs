using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightUpperArmModelChanger : MonoBehaviour
{
    public List<GameObject> Models;

    private void Awake()
    {
        GetAllModels();
    }

    private void GetAllModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            Models.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnequipAllModels()
    {
        foreach (GameObject model in Models)
        {
            model.SetActive(false);
        }
    }

    public void EquipModelByName(string modelName)
    {
        for (int i = 0; i < Models.Count; i++)
        {
            if (Models[i].name == modelName)
            {
                Models[i].SetActive(true);
            }
        }
    }
}
