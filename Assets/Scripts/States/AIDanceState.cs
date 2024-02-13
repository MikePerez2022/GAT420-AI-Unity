using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState
{
	//float timer = 0;

	public AIDanceState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		Debug.Log("Dance");
		agent.Movement.Stop();
		agent.Movement.velocity = Vector3.zero;
		agent.animator.SetTrigger("Dance");
		agent.timer.value = 4;
	}

	public override void OnUpdate()
	{
		//if (Time.time < timer)
		//{
		//	agent.stateMachine.SetState(nameof(AIIdleState));
		//}
	}

	public override void OnExit()
	{
	}
}
