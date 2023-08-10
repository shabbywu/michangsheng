using UnityEngine;

namespace Fungus;

[RequireComponent(typeof(Flowchart))]
public abstract class Variable : MonoBehaviour
{
	[SerializeField]
	protected VariableScope scope;

	[SerializeField]
	protected string key = "";

	public virtual VariableScope Scope
	{
		get
		{
			return scope;
		}
		set
		{
			scope = value;
		}
	}

	public virtual string Key
	{
		get
		{
			return key;
		}
		set
		{
			key = value;
		}
	}

	public abstract void OnReset();
}
