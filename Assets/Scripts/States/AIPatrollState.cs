using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrollState : AIState
{
	Vector3 destinantion;

	public AIPatrollState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new FloatCondition(agent.destinationDistance, Condition.Predicate.LESS, 1));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIChaseState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new BoolCondition(agent.Agent01));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIFleeState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new BoolCondition(agent.Agent01, false));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		agent.Movement.Resume();
		var navNode = AINavNode.GetRandomAINavNode();
		destinantion = navNode.transform.position;
	}

	public override void OnUpdate()
	{
		agent.Movement.MoveTowards(destinantion);
	}

	public override void OnExit()
	{
		
	}
}
