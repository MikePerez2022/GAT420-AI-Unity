using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIFleeState : AIState
{
	float initialSpeed;

	public AIFleeState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.Movement.Resume();
		agent.animator.SetTrigger("Flee");
		initialSpeed = agent.Movement.maxSpeed;
		agent.Movement.maxSpeed *= 2;
	}

	public override void OnUpdate()
	{
		if (agent.enemySeen)
		{
			Vector3 direction = (agent.transform.position - agent.enemy.transform.position) * 1.6f;
			Vector3 desired = direction.normalized * agent.Movement.maxSpeed;
			agent.Movement.MoveTowards(desired);
		}
		else
		{
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	public override void OnExit()
	{
		agent.Movement.maxSpeed = initialSpeed;
	}
}
