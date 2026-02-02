
using GameplayHud;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Hud : MonoBehaviour
{
    public Selecter selecter;
    InventoryHudController inventoryController;
    List<ISlotable> slotables = new List<ISlotable>();
    public bool isBackpack;

    [Inject]
    public void Constructs(InventoryHudController inventoryController, Selecter selecter)
    {
        this.inventoryController = inventoryController;
        this.selecter = selecter;
    }
    private void Awake()
    {
        InitSlotables();
    }
    private void Start()
    {
        inventoryController.Load(2, 0);
        inventoryController.Load(3, 1);        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchCursor();
        }
    }
    public void SwitchCursor()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Confined;
            //selecter.enabled = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            //selecter.enabled = false; 

        }
    }
    void InitSlotables()
    { 
        slotables.Add(inventoryController);

        foreach (var hud in slotables)
        {
            hud.InitSlots();
        }
    }

    public void ToggleBackpack()
    {
        if (isBackpack)
        {
            CloseBackpack();
        }
        else
        {
            OpenBackpack();
        }
    }
    public void OpenBackpack()
    {

        foreach (var hud in slotables)
        {
            hud.Toggle(true);

        }
        isBackpack = true;
    }
    void CloseBackpack()
    {

        foreach (var hud in slotables)
        {
            hud.Toggle(false);
        }
        isBackpack = false;
    }

}
