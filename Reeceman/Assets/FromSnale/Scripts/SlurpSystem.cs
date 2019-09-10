using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlurpSystem : MonoBehaviour {

    public Transform handHomePoint;

    public HandStateController handStateController;

    private int layerMask = 0;
    public bool isOverSnail = false;
    public bool isSlurping = false;
    public bool hasSnail = false;
    private GameObject currOverSnail = null;
    private GameObject currSnail = null;

    public float ejectForce = 500;

    // Use this for initialization
    void Start () {
        // Bit shift the index of the layer (8) to get a bit mask
        layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
    }
	
	// Update is called once per frame
	void Update () {
        doSnailCheck();
        checkSnailEject();
	}
    
    private void doSnailCheck()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(handHomePoint.position, handHomePoint.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            // Draw ray to show what it hit
            Debug.DrawRay(handHomePoint.position, handHomePoint.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

            // If it's a snail
            if (hit.collider.gameObject.CompareTag("Snail"))
            {
                Debug.Log("IS OVER SNAIL");

                // Set local state
                currOverSnail = hit.collider.gameObject;
                setShowingOverSnail(true);



                // ...and they click
                if (Input.GetMouseButtonDown(0) && !isSlurping && !hasSnail)
                {
                    doInslurp(hit.collider.gameObject);
                }
            } else
            {
                // It's not a snail
                setShowingOverSnail(false);
                currOverSnail = null;
            }
        }
        else
        {
            // Draw ray to show what it DID NOT hit
            Debug.DrawRay(handHomePoint.position, handHomePoint.TransformDirection(Vector3.forward) * 1000, Color.white);
            currOverSnail = null;
        }
    }

    private void checkSnailEject()
    {
        if (hasSnail && !isSlurping && Input.GetMouseButtonDown(0))
        {
            EjectSnail();
        }
    }

    private void EjectSnail()
    {
        Debug.Log("Eject");
        currSnail.transform.parent = null;
        Rigidbody rb = currSnail.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = false;
        if (rb) rb.AddForce(handHomePoint.forward * ejectForce);
        currSnail = null;
        hasSnail = false;
    }

    private void setShowingOverSnail(bool isOver)
    {
        if (isOver && !isOverSnail)
        {
            Debug.Log("Showing over");
            handStateController.Excite();

            HighlightController hcl = currOverSnail.GetComponent<HighlightController>();
            if (hcl) hcl.Highlight();
        }

        if (!isOver && isOverSnail)
        {
            Debug.Log("Hiding over");
            handStateController.UnExcite();

            if (currOverSnail)
            {
                HighlightController hcl = currOverSnail.GetComponent<HighlightController>();
                if (hcl) hcl.UnHighlight();
            }
        }

        isOverSnail = isOver;
    }

    private void doInslurp(GameObject snailToSlurp)
    {
        Debug.Log("Is Slupring");
        isSlurping = true;

        StartCoroutine(coSlurp(snailToSlurp, .2f));

        snailToSlurp.transform.parent = handHomePoint;

        currSnail = snailToSlurp;

        Rigidbody rb = currSnail.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;


        hasSnail = true;
    }

    private IEnumerator coSlurp(GameObject snailToSlurp, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos  = snailToSlurp.transform.position;
        while (elapsedTime < time)
        {
            snailToSlurp.transform.position = Vector3.Lerp(startingPos, handHomePoint.transform.position, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        isSlurping = false;
    }
}
