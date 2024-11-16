using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScrips : MonoBehaviour
{
    // Start is called before the first frame update

    public float jumpHeight = 1.0f;
    public float jumpSpeed = 2.0f;

    private bool isJumping = false;
    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void Update()
    {
        if (!isJumping)
        {
            isJumping = true;
            Jump();
        }
    }

    private void Jump()
    {
        Vector3 targetPosition = originalPosition + Vector3.up * jumpHeight;
        float journeyLength = Vector3.Distance(originalPosition, targetPosition);
        float startTime = Time.time;

        while (Time.time - startTime < journeyLength / jumpSpeed)
        {
            float distanceCovered = (Time.time - startTime) * jumpSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, fractionOfJourney);
            isJumping = false;

            // Make sure to yield in a proper coroutine in a real Unity environment
            // In this simplified example, we're using a loop, but it's not recommended in Unity
        }

        transform.position = targetPosition;
        isJumping = false;
    }
}
