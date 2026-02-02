using System;
using UnityEngine;

public class Ui3dController
{
    Ui3dView view;

    public Ui3dController(Ui3dView view)
    {
        this.view = view;
    }

    public void ActivatePickUp(Vector3 pos)
    {
        view.ActivatePickUp(pos);
    }

    internal void DeactivatePickUp()
    {
        view.DeactivatePickUp();
    }
}

