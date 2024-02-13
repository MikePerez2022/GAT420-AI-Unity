using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHitState : AIState
{
	float timer = 0;

	public AIHitState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		agent.Movement.Stop();
		agent.animator.SetTrigger("Hit");

	}

	public override void OnUpdate()
	{
		agent.stateMachine.SetState(nameof(AIFleeState));

	}

	public override void OnExit()
	{

	}
}
