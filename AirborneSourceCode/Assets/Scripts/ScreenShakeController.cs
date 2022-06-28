using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{

    public static ScreenShakeController instance;

    private float shakeDuration;
    private float shakePower;

    private float shakeFade;

    private float shakeRotation;
    public float rotationMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    private void LateUpdate()
    {

        //If the shake is still ongoing....
        if (shakeDuration > 0)
        {

            //Decrement from duration time
            shakeDuration -= Time.deltaTime;

            //Pick a random range between -1 and 1 and multply it by our power
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            //Set position of the camera to these new cords
            transform.position += new Vector3(xAmount, yAmount, 0);

            //Move shakePower towards 0 so that the stop is not as abrupt
            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFade * Time.deltaTime);

            //Add a bit of rotation to the shake
            shakeRotation = Mathf.MoveTowards(shakeRotation, 0f, shakeFade * rotationMultiplier * Time.deltaTime);
        }

        //Apply rotation
        transform.rotation = Quaternion.Euler(0,0,shakeRotation * Random.Range(-1f,1f));
    }

    //Set variables to start the screen shake. Takes in duration and power
    public void StartShake(float duration, float power)
    {
        shakeDuration = duration;
        shakePower = power;
        shakeFade = power / duration;
        shakeRotation = power * rotationMultiplier;

    }
}
