using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ememycontroller2 : EnemyController
{
    private Vector2 AimPosition;
    public List<Vector2> AimPositionList = new List<Vector2>();

    protected override void FixedUpdate()
    {
            GetAnAim();
            MoveX(AimPosition);
            
        
    } 



    void GetAnAim()
    {
        Vector2 test = new Vector2 (6f,-3f);
        if(AimPositionList.Count>0)
        {
            AimPosition = AimPositionList[0];
            AimPositionList.RemoveAt(0);
        }
        else
        {
            AimPosition = test;
        }
    }

}
