using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{
    [Header("Scaner")]
    public float scanRange;
    public LayerMask targetLayer;
    public RaycastHit2D[] target;
    public Transform nearsetTarget;

    

    private void FixedUpdate()
    {
        target = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearsetTarget = GetNearset();
    }

    public Transform GetNearset()
    {
        Transform result = null;
        float diff = 100f;

        foreach (RaycastHit2D item in target)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = item.transform.position;

            float currDiff = Vector3.Distance(myPos, targetPos);
        
            if(currDiff < diff)
            {
                diff = currDiff;
                result = item.transform;
            }
        }

        return result;
    }
}
