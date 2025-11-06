using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class DoorInteraction : MonoBehaviour, IInteractable
{

    public float openAngle = 90f;
    public float openSpeed = 2f;
    public bool isOpen = false;
    public bool lockedByItem = false;
    [SerializeField, ItemSelector]
    private Items requiredItem;
    public bool locked = false;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;

    public void Start()
    {
        closedRotation = transform.rotation;
        openRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
    }

    public void Interact()
    {
        if(currentCoroutine != null) StopCoroutine(currentCoroutine);

        if (lockedByItem && locked) {
            if (Inventory.Instance.HasItem(requiredItem.itemId))
            {
                locked = false;
                currentCoroutine = StartCoroutine(ToggleDoor());
                ToastManager.Instance.ShowToast("Você usou a chave!");
            }
            else
            {
                ToastManager.Instance.ShowToast("Está trancada. Precisa da chave certa.");
                return;
            }

            return;
        }
        currentCoroutine = StartCoroutine(ToggleDoor());
    }

    private IEnumerator ToggleDoor()
    {
        Quaternion targetRotation = isOpen ? closedRotation : openRotation;
        isOpen = !isOpen;

        while(Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}

