using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIIdleState : AIState
{
	//AIStateTransition transition = new AIStateTransition(nameof(AIPatrollState));

	public AIIdleState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIPatrollState));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIChaseState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new BoolCondition(agent.Agent01));
		transitions.Add(transition);


		transition = new AIStateTransition(nameof(AIFleeState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new BoolCondition(agent.Agent01, false));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIWaveState));
		transition.AddCondition(new BoolCondition(agent.friendSeen));
		transitions.Add(transition);
	}

	public override void OnEnter()
	{
		Debug.Log("Idle Enter");
		agent.Movement.Stop();
		agent.timer.value = Random.Range(3,8);
	}

	public override void OnUpdate()
	{
		if(Input.GetKeyDown(KeyCode.D)) agent.stateMachine.SetState(nameof(AIDanceState));

		//if (transition.ToTransition()) agent.stateMachine.SetState(transition.nextState);

		//foreach(var transition in transitions)
		//{
		//	if(transition.ToTransition())
		//	{
		//		agent.stateMachine.SetState(transition.nextState);
		//	}
		//}
		
		//if(agent.tag == "Agent01")//
		//{
		//	agent.stateMachine.SetState(nameof(AIChaseState));
		//}
		//else if (enemies.Length > 0 && agent.tag == "Agent02")
		//{
		//	agent.stateMachine.SetState(nameof(AIFleeState));
		//}
		//
		//var friends = agent.friendPerception.GetGameObjects();
		//if (friends.Length > 0)
		//{
		//	agent.stateMachine.SetState(nameof(AIWaveState));
		//}
	}

	public override void OnExit()
	{
		Debug.Log("Idle Exit");
	}
}
