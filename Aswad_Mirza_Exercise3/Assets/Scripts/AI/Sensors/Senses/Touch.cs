using UnityEngine;

public class Touch : Sense
{

    /*
    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectType != aspectName)
            {
                print("Enemy Touch Detected");
            }
        }
    }
    */
    //In order to keep the collision of the object, and to prevent it from moving through walls it has oncollision enter
    private void OnCollisionEnter(Collision collision)
    {
        Aspect aspect = collision.gameObject.GetComponent<Aspect>();
        if (aspect != null) {

            //Checking if it it touching an object that has a different aspect from its current  aspect, just give different logic for this
            if (aspect.aspectType == aspectName) {
                Debug.Log("Something interesting detected");
            }
        }
    }
}
