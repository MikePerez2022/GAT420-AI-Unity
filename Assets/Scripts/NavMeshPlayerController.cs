using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class NavMeshPlayerController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character = null;

	void Start()
	{
		agent.updateRotation = false;
	}

	void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
        if(character != null)
		{
			if (agent.remainingDistance > agent.stoppingDistance)
			{
				character.Move(agent.desiredVelocity, false, false);
			}
			else
			{
				character.Move(Vector3.zero, false, false);
			}
		}
    }
}
