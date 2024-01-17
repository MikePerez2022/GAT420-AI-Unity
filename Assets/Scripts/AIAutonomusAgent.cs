using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomusAgent : Agent
{
    [SerializeField] AIPerception seekPerception = null;
    [SerializeField] AIPerception fleePerception = null;
    [SerializeField] AIPerception flockPerception = null;
    [SerializeField] AIPerception obstaclePerception = null; 

    private void Update()
    {
        // seek
        if (seekPerception != null) 
        {
			var gameObjects = seekPerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				Movement.ApplyForce(Seek(gameObjects[0]));
			}
		}

		// Flee
		if (fleePerception != null)
		{
			var gameObjects = fleePerception.GetGameObjects();
			if (gameObjects.Length > 0)
			{
				Vector3 force = Flee(gameObjects[0]);
				Movement.ApplyForce(force);
			}
		}

		// Flock
		if (flockPerception != null)
		{
			var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
			{
				Movement.ApplyForce(Cohesion(gameObjects));
				Movement.ApplyForce(Seperation(gameObjects, 3));
				Movement.ApplyForce(Alignment(gameObjects));
			}
		}

		//obstacle avoidence
		if (obstaclePerception != null)
		{
			if (((AISpherecastPerception)obstaclePerception).CheckDirection(Vector3.forward))
			{
				Vector3 open = Vector3.zero;
				if (((AISpherecastPerception)obstaclePerception).GetOpenDirection(ref open))
				{
					Movement.ApplyForce(GetSteeringForce(open) * 5);
				}
			}
		}

		Vector3 acceleration = Movement.acceleration;
		acceleration.y = 0;
		Movement.acceleration = acceleration;

		transform.position = Utilities.wrap(transform.position, new Vector3(-10,-10,-10), new Vector3(10, 10, 10));
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

	private Vector3 Flee(GameObject target)
	{
		Vector3 direction = transform.position - target.transform.position;
		Vector3 force = GetSteeringForce(direction);

		return force;
	}

	private Vector3 Cohesion(GameObject[] neighbors)
	{
		Vector3 positions = Vector3.zero;

		foreach(var neighbor in neighbors) 
		{
			positions += neighbor.transform.position;
		}

		Vector3 center = positions / neighbors.Length;
		Vector3 direction = center - transform.position;
		Vector3 force = GetSteeringForce(direction);

		return force;
	}

	private Vector3 Seperation(GameObject[] neighbors, float radius)
	{
		Vector3 seperation = Vector3.zero;
		foreach(var neighbor in neighbors)
		{
			Vector3 direction = (transform.position - neighbor.transform.position);
			if(direction.magnitude < radius)
			{
				seperation += (direction / direction.sqrMagnitude) * 10;
			}
		}

		Vector3 force = GetSteeringForce(seperation);

		return force;
	}

	private Vector3 Alignment(GameObject[] neighbors)
	{
		Vector3 velocities = Vector3.zero;

		foreach (var neighbor in neighbors)
		{
			velocities += neighbor.GetComponent<Agent>().Movement.velocity;
		}

		Vector3 avgVelocity = velocities / neighbors.Length;

		Vector3 force = GetSteeringForce(avgVelocity);

		return force;
	}

	private Vector3 GetSteeringForce(Vector3 direction)
	{
		Vector3 desired = direction.normalized * Movement.maxSpeed;
		Vector3 steer = desired - Movement.velocity;
		Vector3 force = Vector3.ClampMagnitude(steer, Movement.maxForce);

		return force;
	}
}
