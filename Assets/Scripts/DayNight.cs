using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 1f; // Speed of the rotation
    private Light directionalLight; // Reference to the Light component

    [SerializeField]
    private float maxIntensity = 2f; // Max intensity of the light
    [SerializeField]
    private float minIntensity = 0.5f; // Min intensity of the light (dimmed state)

    // Start is called before the first frame update
    void Start()
    {
        directionalLight = GetComponent<Light>(); // Get the Light component attached to this GameObject
        StartCoroutine(RotateAndDimLight());
    }

    IEnumerator RotateAndDimLight()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = transform.rotation * Quaternion.Euler(0, 160, 0);

        while (true) // Infinite loop to keep the effect running
        {
            // Rotate and increase light intensity
            float timeElapsed = 0;
            while (timeElapsed < 1f)
            {
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, timeElapsed);
                directionalLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, timeElapsed); // Increase intensity
                timeElapsed += Time.deltaTime * rotationSpeed;
                yield return null;
            }
            transform.rotation = endRotation;

            // Dim the light instantly after reaching the end of the rotation
            directionalLight.intensity = minIntensity;

            // Swap the start and end rotations for the next cycle
            (startRotation, endRotation) = (endRotation, startRotation);

            // Add a delay here if desired, to simulate night
            yield return new WaitForSeconds(1f); // Simulate a brief night before starting again
        }
    }
}
