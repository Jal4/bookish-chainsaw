using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorsoModelChanger : MonoBehaviour
{
    public List<GameObject> TorsoModels;

    private void Awake()
    {
        GetAllTorsoModels();
    }

    private void GetAllTorsoModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            TorsoModels.Add(transform.GetChild(i).gameObject);
        }
    }

    public void UnequipAllTorsoModels()
    {
        foreach (GameObject torsoModel in TorsoModels)
        {
            torsoModel.SetActive(false);
        }
    }

    public void EquipTorsoModelByName(string torsoName)
    {
        for (int i = 0; i < TorsoModels.Count; i++)
        {
            if (TorsoModels[i].name == torsoName)
            {
                TorsoModels[i].SetActive(true);
            }
        }
    }
}
