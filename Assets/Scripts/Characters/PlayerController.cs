using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavGame.Core;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerController : TouchableGameObject
{
    NavMeshAgent agent;
    Camera cam;
    public LayerMask walkableLayer;
    public LayerMask collectibleLayer;

    CollectibleGameObject pickupTarget;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        ProcessInput();
        UpdateCollect();
    }

    void ProcessInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
            {
                agent.SetDestination(hit.point);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, collectibleLayer))
            {
                pickupTarget = hit.collider.gameObject.GetComponent<CollectibleGameObject>();
                agent.SetDestination(hit.point);
            }
            else 
            {
                pickupTarget = null;
            }
        }
    }

    void UpdateCollect()
    {
        if (pickupTarget != null) 
        {
            if (IsInTouch(pickupTarget))
            {
                pickupTarget.Pickup();
            }
        }
    }
}
