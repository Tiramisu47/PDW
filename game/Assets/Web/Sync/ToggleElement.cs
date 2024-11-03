using UnityEngine;

public class ToggleElement : MonoBehaviour
{
    protected bool state = false;
    public int uid;

    public virtual void Toggle()
    {
        state = !state;
        Debug.Log($"Element {uid} toggled. New state: {state}");
    }

    // public virtual bool GetState()
    // {
    //     Debug.Log($"Element {uid} state: {state}");
    //     return state;
    // }
}
