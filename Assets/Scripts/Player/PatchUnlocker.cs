using System.Collections;
using UnityEngine;

public class PatchUnlocker : CollisionEvents
{
    private bool isInside;
    private BoundInformation _bi;
    
    public override void CollisionEnter(Collision triggeredObject)
    {
        _bi = triggeredObject.collider.GetComponent<BoundInformation>();
        isInside = true;
        StartCoroutine(DeductMoneyByFrame());
    }

    public override void CollisionExit(Collision triggeredObject)
    {
        isInside = false;
    }

    public override void CollisionStay(Collision triggeredObject)
    {
    }

    IEnumerator DeductMoneyByFrame()
    {
        while (isInside)
        {
            yield return new WaitForSeconds(0.05f);
            if (_bi != null)
            {
                if (_bi._otherPatch._linkerScript.DeductMoney(1))
                {
                    // means all money deducted
                    _bi._otherPatch._linkerScript.isLocked = false;
                    // toggle patch visibility
                    _bi._otherPatch._linkerScript.SetPatchVisibility(true);
                    //refresh all side bounds for collider
                    _bi._otherPatch._linkerScript.RefreshBounds();
                    _bi._myPatch._linkerScript.RefreshBounds();
                    // enable side patches UI to show currency
                    _bi._otherPatch._linkerScript.SetBoundingPatchVisibility();
                    // update state to local storage
                    _bi._otherPatch._linkerScript.UpdateLockState();
                    
                    isInside = false;
                }
            }
        }
    }
}