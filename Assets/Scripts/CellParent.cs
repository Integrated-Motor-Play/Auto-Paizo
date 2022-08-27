using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;

public class CellParent : MonoBehaviour
{
    private void Awake()
    {
        var cells = GameManager.Instance.GetComponentsInChildren<EMSConnectCell>();
        if(!cells.Any()) return;
        var myCells = GetComponentsInChildren<EMSConnectCell>();
        foreach (var cell in cells)
        {
            var myCell = myCells.ToList().Find(c => c.index == cell.index);
            if(myCell == null) continue;
            cell.transform.SetParent(transform);
            cell.transform.position = myCell.transform.position;
            Destroy(myCell.gameObject);
        }
        var leftCells = GameManager.Instance.GetComponentsInChildren<EMSConnectCell>();
        foreach (var cell in leftCells)
        {
            Destroy(cell.gameObject);
        }
    }
}
