using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState
{
	float timer = 0;

	public AIWaveState(AIStateAgent agent) : base(agent)
	{
	}

	public override void OnEnter()
	{
		Debug.Log("Wave");
		agent.Movement.Stop();
		agent.Movement.velocity = Vector3.zero;
		agent.transform.LookAt(agent.friend.transform.position,Vector3.up);
		agent.animator.SetTrigger("Wave");
		timer = Time.time + 1;
	}

	public override void OnUpdate()
	{
		if(Time.time > timer)
		{
			agent.transform.Rotate(0, 180, 0);
			agent.stateMachine.SetState(nameof(AIIdleState));
		}
	}

	public override void OnExit()
	{
	}
}
