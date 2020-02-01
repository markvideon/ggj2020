using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTechnician : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float distanceThreshold = 0.1f;
    [SerializeField] private float moveRate = 2f;

    [SerializeField] public bool active = false;

    void Update()
    {
        if (active)
        {
            if (Vector3.Distance(this.transform.position, target.transform.position) > distanceThreshold)
            {
                Vector3 differential = target.transform.position - this.transform.position;
                var nextX = differential.x * moveRate * Time.deltaTime;
                var nextY = differential.y * moveRate * Time.deltaTime;
                this.transform.position += new Vector3(nextX, nextY, 0f);
            }
        }
        
    }

    public void SetTarget(Transform nextTarget) => target = nextTarget;
    public void SetMoveRate(float nextRate) => moveRate = nextRate;
    public void SetDistanceThreshold(float nextThreshold) => distanceThreshold = nextThreshold;
    
}
