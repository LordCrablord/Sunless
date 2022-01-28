using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera myCamera;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private GameObject mouseClick;
    [SerializeField] private GameObject playerTarget;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundLayer))
            {
                GameObject click = Instantiate(mouseClick);
                click.transform.position = raycastHit.point;
                playerTarget.transform.position = raycastHit.point;

                GetComponent<Unit>().target = playerTarget.transform;
            }
        }
    }
}
