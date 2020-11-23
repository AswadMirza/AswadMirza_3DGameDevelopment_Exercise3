using UnityEngine;

public class Wander : MonoBehaviour 
{
    private Vector3 targetPosition;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 2.0f;
    public float targetPositionTolerance = 3.0f;
    public float minX = -55f;
    public float maxX =5f;
    public float minZ=-5f;
    public float maxZ=5f;

	void Start () 
    {
        /*
         * minX = -45.0f;
        maxX = 45.0f;

        minZ = -45.0f;
        maxZ = 45.0f;
         
         */



        //Get Wander Position
        GetNextPosition();
	}
	
	void Update () 
    {
        if(Vector3.Distance(targetPosition, transform.position) <= targetPositionTolerance) 
        {
            GetNextPosition();
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
	}

    public void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    public void Follow(Transform followTarget) {
        targetPosition = followTarget.position;
    }
}
