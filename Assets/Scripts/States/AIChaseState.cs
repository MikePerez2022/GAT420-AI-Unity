using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIChaseState : AIState
{
	float initialSpeed;

	public AIChaseState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIAttackState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new FloatCondition(agent.enemyDistance, Condition.Predicate.LESS, 1));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new BoolCondition(agent.enemySeen, false));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		agent.Movement.Resume();
		agent.animator.SetTrigger("Chase");
		initialSpeed = agent.Movement.maxSpeed;
		agent.Movement.maxSpeed *= 2;
	}

	public override void OnUpdate()
	{
		if(agent.enemySeen) agent.Movement.MoveTowards(agent.enemy.transform.position);

		//Might not need this - Below
		//foreach(var transition in transitions)
		//{
		//	if (transition.ToTransition()) agent.stateMachine.SetState(transition.nextState);
		//}

		//var enemies = agent.enemyPerception.GetGameObjects();
		//if (enemies.Length > 0)
		//{
		//	var enemy = enemies[0];
		//	agent.Movement.MoveTowards(enemy.transform.position);
		//	if (Vector3.Distance(agent.transform.position, enemy.transform.position) < 1.25f)
		//	{
		//		agent.stateMachine.SetState(nameof(AIAttackState));
		//	}
		//	else if(Vector3.Distance(agent.transform.position, enemy.transform.position) > 6.25f)
		//	{
		//		agent.stateMachine.SetState(nameof(AIIdleState));
		//	}
		//}
		//else
		//{
		//	agent.stateMachine.SetState(nameof(AIIdleState));
		//}
	}

	public override void OnExit()
	{
		agent.Movement.maxSpeed = initialSpeed;
	}
}
