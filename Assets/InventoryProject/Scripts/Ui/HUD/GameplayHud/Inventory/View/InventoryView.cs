using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class InventoryView : MonoBehaviour
{
    public Transform slotsParent;
    public List<PullableSlot> pullableSlots;
    Selecter selecter;
    [Inject]
    public void Contruct(Selecter selecter)
    {
        this.selecter = selecter;
    }
    private void Awake()
    {
        pullableSlots = slotsParent.GetComponentsInChildren<PullableSlot>(true).ToList();
       
    }
    public void Activate()
    {
        slotsParent.gameObject.SetActive(true);
        selecter.enabled = true;
    }
    public void Deactivate()
    {
        slotsParent.gameObject.SetActive(false);
        selecter.enabled = false;
    }
}
