using System;
using System.Collections;
using System.Collections.Generic;
using Turrets;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;

    public Turret Turret { get; set; }

    public bool IsEmpty => Turret == null;
    
    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }

    public void SelectTurret()
    {
        OnNodeSelected?.Invoke(this);
    }
}
