using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{
	public AIDeathState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.Movement.Stop();
		agent.Movement.velocity = Vector3.zero;

		agent.animator.SetTrigger("Death");
		agent.timer.value = Time.time + 5;
	}

	public override void OnUpdate()
	{
		if (agent.timer <= 0)
		{
			GameObject.Destroy(agent.gameObject);
		}
	}

	public override void OnExit()
	{
	}
}
