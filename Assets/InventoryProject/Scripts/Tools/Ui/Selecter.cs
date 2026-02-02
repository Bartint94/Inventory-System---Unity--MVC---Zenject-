using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;

public class Selecter : MonoBehaviour
{
    IDragable dragable = null;
    public bool hadDragable => dragable != null;
    public void SetDragableOutside(IDragable value)
    {
        dragable = value;
    }
    void Update()
    {
      
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            if(dragable != null) { return; }
            // Create a pointer event animationsData for the current mouse position
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            
            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast against all GraphicRaycasters in the scene
            EventSystem.current.RaycastAll(pointerEventData, results);

            // Check if any UI element was hit
            if(results.Count > 0)
            if (results[0].gameObject.TryGetComponent(out IDragable dragable))
            {
                this.dragable = dragable;
                dragable.Drag();
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            if (dragable == null) return;
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;


            List<RaycastResult> results = new List<RaycastResult>();

            // Raycast against all GraphicRaycasters in the scene
            EventSystem.current.RaycastAll(pointerEventData, results);

            bool isPull = false;
            foreach (RaycastResult result in results)
            {

                if (result.gameObject.TryGetComponent(out IPullable pullable))
                {
                    pullable.Pull(dragable);
                    
                    dragable = null;
                    isPull = true;

                }
            }
            if (!isPull)
            {
                dragable.DropOut();
                dragable = null;
                print("drop");
            }

        }
    }
}
