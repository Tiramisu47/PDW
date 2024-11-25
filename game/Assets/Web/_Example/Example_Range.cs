using UnityEngine;

public class Example_Range : RangeElement
{
    [SerializeField] private Transform box;
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 targetPosition;

    public override void NewValue(int newValue)
    {
        base.NewValue(newValue);

        float y = Mathf.Lerp(1f, 3f, newValue / 100f);
        targetPosition = new Vector3(box.position.x, y, box.position.z);
    }

    void Start()
    {
        targetPosition = new Vector3(box.position.x, box.position.y, box.position.z);
    }

    void Update()
    {
        if (box == null) return;
        box.position = Vector3.Lerp(box.position, targetPosition, Time.deltaTime * moveSpeed);
    }
}
