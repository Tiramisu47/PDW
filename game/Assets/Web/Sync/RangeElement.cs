using UnityEngine;

public class RangeElement : MonoBehaviour
{
    protected int value = 0;
    public int uid;

    public virtual void NewValue(int newValue)
    {
        value = newValue;
        Debug.Log($"Element {uid}. New value: {value}");
    }

    // public virtual bool GetState()
    // {
    //     Debug.Log($"Element {uid} state: {state}");
    //     return state;
    // }
}
