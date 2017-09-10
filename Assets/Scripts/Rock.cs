using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour {

    [SerializeField]
    private Transform topPos;

    [SerializeField]
    private Transform bottomPos;
    
    [SerializeField]
    private float movingSpeed = 10.0f;

    private Vector3 target;

    private void Start()
    {
        // set up the start up moving target
        target = SetTarget();
        // start up the coroutine
        StartCoroutine("Move");
    }

    /**
     * this coroutine method make the rock drift between to point non-stop
     */
    IEnumerator Move() {
    
        while(DistanceTo(target) > 0.2f) {
            // move toward the target
            transform.Translate(Vector3.Normalize(target - transform.localPosition) * movingSpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        
        // reset target
        target = SetTarget();
        // restart coroutine
        StartCoroutine("Move");
    }
    
    /**
     * the absolute distance between two vector
     */
    private float DistanceTo(Vector3 target) {
        return Mathf.Abs(Vector3.Distance(transform.localPosition, target));
    }
    
    /**
     * the target between two vector3 points
     */
    private Vector3 SetTarget() {
        return this.DistanceTo(topPos.localPosition) > this.DistanceTo(bottomPos.localPosition) ? 
        topPos.localPosition : bottomPos.localPosition;
    }


}
