using UnityEngine;
using UnityEngine.Events;

public class AtrCallBack : MonoBehaviour
{
	private bool _isInit;

	private UnityAction _action;

	private void OnEnable()
	{
		if (!_isInit)
		{
			_isInit = true;
			((Behaviour)this).enabled = false;
		}
		else if (_action != null)
		{
			_action.Invoke();
			((Behaviour)this).enabled = false;
			_action = null;
		}
	}

	public void SetAction(UnityAction action)
	{
		_action = action;
	}
}
