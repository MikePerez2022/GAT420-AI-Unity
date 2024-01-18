using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AINavPath))]
public class AINavAgent : Agent
{
	[SerializeField] private AINavPath path;

	void Update()
	{
		if (path.HasPath())
		{
			Debug.DrawLine(transform.position, path.destination);
			Movement.MoveTowards(path.destination);
		}
	}
}
