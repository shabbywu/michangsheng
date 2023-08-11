using UnityEngine;
using UnityEngine.Events;

namespace PaiMai;

public class PlayerCommand : MonoBehaviour
{
	private FpBtn _btn;

	private Animator _animator;

	public CommandType CommandType;

	public CeLueType CeLueType;

	private UnityAction _toBack;

	public bool CanClick { get; private set; }

	public void AddClickAction(UnityAction action)
	{
		if ((Object)(object)_btn == (Object)null)
		{
			_btn = ((Component)this).GetComponentInChildren<FpBtn>();
			_btn.mouseUpEvent.AddListener(action);
		}
		if ((Object)(object)_animator == (Object)null)
		{
			_animator = ((Component)this).GetComponent<Animator>();
		}
	}

	public void AddMouseEnterListener(UnityAction action)
	{
		if ((Object)(object)_btn == (Object)null)
		{
			_btn = ((Component)this).GetComponentInChildren<FpBtn>();
		}
		_btn.mouseEnterEvent.AddListener(action);
	}

	public void AddMouseOutsListener(UnityAction action)
	{
		if ((Object)(object)_btn == (Object)null)
		{
			_btn = ((Component)this).GetComponentInChildren<FpBtn>();
		}
		_btn.mouseOutEvent.AddListener(action);
	}

	public void PlayToBack()
	{
		_btn.SetCanClick(flag: false);
		CanClick = false;
		_animator.Play("ToBack");
	}

	public void PlayBackTo()
	{
		_animator.Play("BackTo");
	}

	public void BackTo()
	{
		CanClick = true;
		_btn.SetCanClick(flag: true);
	}
}
