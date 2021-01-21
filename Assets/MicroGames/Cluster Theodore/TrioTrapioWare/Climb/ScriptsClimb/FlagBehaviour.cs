using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagBehaviour : MonoBehaviour
{
   


    [SerializeField] private Material material;

    private void Start()
    {
        material = gameObject.GetComponent<SpriteRenderer>().material;
    }
    // Update is called once per frame
    void Update()
    {
        if (TrapioWare.Climb.ClimbGameManager.Instance.needToStop)
        {
            material.SetFloat("Vector1_B204D08E", 0f);
        }

    }
}
