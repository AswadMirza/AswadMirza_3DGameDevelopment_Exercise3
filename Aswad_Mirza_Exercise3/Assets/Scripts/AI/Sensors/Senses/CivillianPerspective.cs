using System.Collections;
using UnityEngine;

public class CivillianPerspective : Sense
{
    public int fieldOfView = 45;
    public int viewDistance = 100;


    public Transform apectPerspectiveTransform;
    //public Transform[] interestingObjects;
    private Vector3 rayDirection;

    // the distance in which this car needs to slow down when it detects a civillian in front of it
    public float followingDistance = 10f;
    
    protected override void Initialize()
    {
        // this line of code is problematic because it is hardcoded for it to detect the player or enemy, 
        //it should be dynamic and able to detect whatever you want it to detect
        //apectPerspectiveTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //playerTransform = GameObject.Find("Civilian_Test").transform;
        //GameObject[] civillianObjects = GameObject.FindGameObjectsWithTag("Civillian");

         

    }

    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= detectionRate)
        {
            DetectAspect();

            /*
            for (int i = 0; i < interestingObjects.Length; i++) {
                DetectAspect(interestingObjects[i]);
            }
            */
        }
    }

    //Detect perspective field of view for the AI Character
    void DetectAspect()
    {
        RaycastHit hit;
        rayDirection = apectPerspectiveTransform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            // Detect if object is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();

                // logic for the object it is interested in
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        print($"{gameObject.name} Detected a {aspect.aspectType} it is interested in ");

                        if (Vector3.Distance(apectPerspectiveTransform.position, transform.position) < followingDistance)
                        {
                            interestingObjectDetected();
                        }

                        //redundant because the wander script will make them take a new direction if they are close enough to thier goal
                        else {
                            interestingObjectNotDetected();
                        }
                        
                    }
                }
                // logic in case it loses vision of the object it is interested in
                else
                {
                    
                     

                }
            }



        }

    }

    void DetectAspect(Transform aspectTransform)
    {



        RaycastHit hit;
        rayDirection = aspectTransform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < fieldOfView)
        {
            // Detect if player is within the field of view
            if (Physics.Raycast(transform.position, rayDirection, out hit, viewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();
                if (aspect != null)
                {
                    //Check the aspect
                    if (aspect.aspectType == aspectName)
                    {
                        print($"{aspectName} Detected");

                    }
                }
            }
        }
    }



    /// <summary>
    /// Show Debug Grids and obstacles inside the editor
    /// </summary>
    void OnDrawGizmos()
    {
        if (apectPerspectiveTransform == null)
        {
            return;
        }

        Debug.DrawLine(transform.position, apectPerspectiveTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * viewDistance);

        //Approximate perspective visualization
        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += fieldOfView * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x -= fieldOfView * 0.5f;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }

    // helper functions to return bool values
     

    bool IsCivillian()
    {
        if (gameObject.tag.Equals("Civillian"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // logic for if the civillian sees the player
    void interestingObjectDetected() {
        Wander wander = gameObject.GetComponent<Wander>();

        if (wander != null) {
            wander.Follow(apectPerspectiveTransform);
        }
    }
    // Logic for if the civillian loses sight of the player;
    void interestingObjectNotDetected() {
        Wander wander = gameObject.GetComponent<Wander>();

        if (wander != null)
        {
            wander.GetNextPosition();
        }
    }

    
    
}
