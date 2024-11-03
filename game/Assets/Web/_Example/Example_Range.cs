using UnityEngine;

public class Example_Range : RangeElement
{
    [SerializeField] Transform box;
    public override void NewValue(int newValue)
    {
        base.NewValue(newValue);

        float x = Mathf.Lerp(0f, 2f, newValue / 100f);
        box.transform.position = new Vector3(0, x, 0);
    }
}
