using UnityEngine;

public class Example_Door : ToggleElement
{
    public float rotationSpeed = 2.0f;
    private float targetRotation = 0.0f;

    public override void Toggle()
    {
        base.Toggle();

        targetRotation = state ? 90.0f : 0.0f;
    }

    void Update()
    {
        float currentRotation = transform.eulerAngles.y;
        float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newRotation, transform.eulerAngles.z);
    }
}
