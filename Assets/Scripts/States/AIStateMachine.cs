using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class AIStateMachine
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>();

	public AIState CurrentState { get; private set; } = null;

	public void Update()
	{
		CurrentState?.OnUpdate();
	}

	public void AddState(string name, AIState state)
	{
		Debug.Assert(!states.ContainsKey(name), "State Machine already contains State " + name);
		states[name] = state;
	}

	public void SetState(string name)
	{
		Debug.Assert(states.ContainsKey(name), "State Machine does not contain State " + name);

		var state = states[name];

		//don't re-enter same state
		if (CurrentState == state) return;

		//exit current state
		CurrentState?.OnExit();
		//set current state
		CurrentState = state;
		//enter new state
		CurrentState?.OnEnter();
	}
}
