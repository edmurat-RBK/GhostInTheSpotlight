using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadderManager : MonoBehaviour
{
    private void Update()
    {
        if (TrapioWare.Climb.ClimbGameManager.Instance.needToCheck)
        {
            Debug.Log("Check");
            UpdateList();
            
        }
    }


    private void UpdateList()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            TrapioWare.Climb.ClimbGameManager.Instance.positions.Add(gameObject.transform.GetChild(i).gameObject);
        }

        TrapioWare.Climb.ClimbGameManager.Instance.needToCheck = false;
        TrapioWare.Climb.ClimbGameManager.Instance.finishInstantiate = true;
    }
}
