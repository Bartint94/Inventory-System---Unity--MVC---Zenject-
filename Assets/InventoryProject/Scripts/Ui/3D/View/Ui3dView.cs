using System;
using UnityEngine;

public class Ui3dView : MonoBehaviour
{
    [SerializeField] GameObject pickUp;
    [SerializeField] float interactionOffset = 1f;
    public void ActivatePickUp(Vector3 pos)
    {
        if (!pickUp.gameObject.activeSelf)
        {
            pickUp.SetActive(true);
        }
        pickUp.transform.position = pos + Vector3.up * interactionOffset;
        pickUp.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
    }

    internal void DeactivatePickUp()
    {
        pickUp.SetActive(false);
    }
}
