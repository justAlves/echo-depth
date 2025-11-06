using UnityEngine;


interface IInteractable
{
    public void Interact();
}

public class Interactor : MonoBehaviour
{

    public Transform interactorSource;
    public float interactorRange;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new(interactorSource.position, interactorSource.forward);
            if(Physics.Raycast(r, out RaycastHit hit, interactorRange))
            {
                if(hit.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}
