using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpManager : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, mainCamera, ItemContainer;

    public float pickUpRange;

    public bool equipped;
    public bool onFloor = true;
    public bool pickedUp1, pickedUp2, pickedUp3;
    public static bool slotFull1, slotFull2, slotFull3;

    private static GameObject currentlyEquippedItem;

    private Renderer objectRenderer;
    private Collider objectCollider;

    private Vector3 localOffset = Vector3.zero;
    private Quaternion localRotation = Quaternion.identity;
    private Stack<int> collectedItems = new Stack<int>(); // Stos przechowuj¹cy numery slotów

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();

        if (!pickedUp1)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (!pickedUp2)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (!pickedUp3)
        {
            rb.isKinematic = false;
            coll.isTrigger = false;
        }

        if (pickedUp1)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }

        if (pickedUp2)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }

        if (pickedUp3)
        {
            rb.isKinematic = true;
            coll.isTrigger = true;
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull1)
        {
            slotFull1 = true;
            pickedUp1 = true;
            EquipItem();
        }
        else if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull2)
        {
            slotFull2 = true;
            pickedUp2 = true;
            EquipItem();
        }
        else if (distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull3)
        {
            slotFull3 = true;
            pickedUp3 = true;
            EquipItem();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (equipped)
            {
                Drop();
                UpdateVisibility();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && slotFull1)
        {
            EquipSpecificItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && slotFull2)
        {
            EquipSpecificItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && slotFull3)
        {
            EquipSpecificItem(3);
        }
    }

    private void LateUpdate()
    {
        if (equipped)
        {
            transform.position = ItemContainer.position + ItemContainer.TransformDirection(localOffset);
            transform.rotation = ItemContainer.rotation * localRotation;
        }
    }

    private void EquipItem()
    {
        equipped = true;

        if (currentlyEquippedItem != null && currentlyEquippedItem != gameObject)
        {
            ItemPickUpManager otherItem = currentlyEquippedItem.GetComponent<ItemPickUpManager>();
            if (otherItem != null)
            {
                otherItem.equipped = false;
                otherItem.UpdateVisibility();
            }
        }

        currentlyEquippedItem = gameObject;
        onFloor = false;

        transform.SetParent(ItemContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        rb.isKinematic = true;
        coll.isTrigger = true;
        BoxCollider boxCollider = coll.GetComponent<BoxCollider>();
        boxCollider.enabled = false;

        if (pickedUp1) collectedItems.Push(1);
        else if (pickedUp2) collectedItems.Push(2);
        else if (pickedUp3) collectedItems.Push(3);

        UpdateVisibility();
    }

    private void EquipSpecificItem(int slotNumber)
    {
        if (slotNumber == 1 && pickedUp1)
        {
            equipped = true;
            EquipItem();
        }
        else if (slotNumber == 2 && pickedUp2)
        {
            equipped = true;
            EquipItem();
        }
        else if (slotNumber == 3 && pickedUp3)
        {
            equipped = true;
            EquipItem();
        }
    }

    private void Drop()
    {
        if (collectedItems.Count == 0)
        {
            Debug.LogWarning("Brak przedmiotów do wyrzucenia!");
            return; // Jeœli stos jest pusty, nic nie rób
        }
        int slotToDrop = collectedItems.Pop(); // Pobierz ostatnio zebran¹ wartoœæ

        // Aktualizuj flagi dla odpowiedniego slotu
        if (slotToDrop == 1)
        {
            pickedUp1 = false;
            slotFull1 = false;
        }
        else if (slotToDrop == 2)
        {
            pickedUp2 = false;
            slotFull2 = false;
        }
        else if (slotToDrop == 3)
        {
            pickedUp3 = false;
            slotFull3 = false;
        }

        equipped = false;
        onFloor = true;

        transform.SetParent(null);
        SetVisibility(true);

        rb.isKinematic = false;
        coll.isTrigger = false;

        BoxCollider boxCollider = coll.GetComponent<BoxCollider>();
        boxCollider.enabled = true;

        // Zerujemy prêdkoœæ
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Ustaw pozycjê przed graczem
        Vector3 dropPosition = player.position + mainCamera.forward * 2f;
        dropPosition.y = Mathf.Max(dropPosition.y, player.position.y + 0.5f); // Minimalna wysokoœæ
        transform.position = dropPosition;

        
    }

    private void UpdateVisibility()
    {
        if (equipped)
        {
            SetVisibility(true);
        }
        else if (!onFloor && !equipped)
        {
            SetVisibility(false);
        }
    }

    private void SetVisibility(bool isVisible)
    {
        if (objectRenderer != null)
        {
            objectRenderer.enabled = isVisible;
        }

        if (objectCollider != null)
        {
            objectCollider.enabled = isVisible;
        }
    }
}
