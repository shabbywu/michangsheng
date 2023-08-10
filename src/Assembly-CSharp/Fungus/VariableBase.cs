using UnityEngine;

namespace Fungus;

public abstract class VariableBase<T> : Variable
{
	private VariableBase<T> _globalStaicRef;

	[SerializeField]
	protected T value;

	protected T startValue;

	private VariableBase<T> globalStaicRef
	{
		get
		{
			if ((Object)(object)_globalStaicRef != (Object)null)
			{
				return _globalStaicRef;
			}
			if (Application.isPlaying)
			{
				return _globalStaicRef = FungusManager.Instance.GlobalVariables.GetOrAddVariable(Key, value, ((object)this).GetType());
			}
			return null;
		}
	}

	public virtual T Value
	{
		get
		{
			if (scope != VariableScope.Global || !Application.isPlaying)
			{
				return value;
			}
			return globalStaicRef.value;
		}
		set
		{
			if (scope != VariableScope.Global || !Application.isPlaying)
			{
				this.value = value;
			}
			else
			{
				globalStaicRef.Value = value;
			}
		}
	}

	public override void OnReset()
	{
		Value = startValue;
	}

	public override string ToString()
	{
		return Value.ToString();
	}

	protected virtual void Start()
	{
		startValue = Value;
	}

	public virtual void Apply(SetOperator setOperator, T value)
	{
		Debug.LogError((object)"Variable doesn't have any operators.");
	}
}
