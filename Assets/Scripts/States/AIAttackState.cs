using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIAttackState : AIState
{
	public AIAttackState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIChaseState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		agent.Movement.Stop();
		agent.Movement.velocity = Vector3.zero;
		agent.animator.SetTrigger("Attack");

		agent.timer.value = 2;
	}

	public override void OnUpdate()
	{
		//if (agent.tag == "Agent02") agent.stateMachine.SetState(nameof(AIFleeState));
		//foreach (var transition in transitions)
		//{
		//	if (transition.ToTransition()) agent.stateMachine.SetState(transition.nextState);
		//}
	}

	public override void OnExit()
	{

	}
}
