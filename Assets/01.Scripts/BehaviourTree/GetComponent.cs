using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class GetComponent<T> : Action where T : Component
{
	SharedVariable<Transform> target;
	SharedVariable<T> component;
	public override void OnStart()
	{

	}

	public override TaskStatus OnUpdate()
	{
		if (target.Value != null)
		{
			component.Value = target.Value.GetComponent<T>();
			return TaskStatus.Success;
		}
		else
		{
			component.Value = GetComponent<T>();
			return TaskStatus.Success;
		}
	}
}