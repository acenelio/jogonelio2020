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

    public float range = 4f;

    public LayerMask walkableLayer;
    public LayerMask collectibleLayer;

    CollectibleGameObject pickupTarget;

    Vector3 actionPoint = Vector3.zero;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        cam = Camera.main;
    }

    void Update()
    {
        ProcessInput();
        UpdateCollect();
        UpdateAction();
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
        else if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, walkableLayer))
            {
                actionPoint = hit.point;
                agent.SetDestination(hit.point);
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

    void UpdateAction()
    {
        if (actionPoint != Vector3.zero)
        {
            if (Vector3.Distance(transform.position, actionPoint) <= range)
            {
                agent.ResetPath();
                actionPoint = Vector3.zero;
            }
        }
    }
}
