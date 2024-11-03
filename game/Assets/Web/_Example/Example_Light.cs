using UnityEngine;

public class Example_Light : ToggleElement
{
    [SerializeField] Material defaultMaterial;
    [SerializeField] Material emissionMaterial;
    [SerializeField] GameObject lightObject;
    [SerializeField] MeshRenderer meshRenderer;


    public override void Toggle()
    {
        base.Toggle(); // Wywołanie oryginalnej metody z klasy bazowej

        // Dodanie własnej logiki
        Debug.Log("Advanced logic after toggling.");
        lightObject.SetActive(state);
        meshRenderer.material = state ? emissionMaterial : defaultMaterial;

    }
}
