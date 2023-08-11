using UnityEngine;

namespace Fungus.Examples;

public class FPDemoPriorityRouter : MonoBehaviour
{
	public Behaviour[] componentEnabledOutsideFungusPriority;

	public Behaviour[] componentEnabledInsideFungusPriority;

	private void OnEnable()
	{
		FungusPrioritySignals.OnFungusPriorityStart += FungusPrioritySignals_OnFungusPriorityStart;
		FungusPrioritySignals.OnFungusPriorityEnd += FungusPrioritySignals_OnFungusPriorityEnd;
	}

	private void OnDisable()
	{
		FungusPrioritySignals.OnFungusPriorityStart -= FungusPrioritySignals_OnFungusPriorityStart;
		FungusPrioritySignals.OnFungusPriorityEnd -= FungusPrioritySignals_OnFungusPriorityEnd;
	}

	private void FungusPrioritySignals_OnFungusPriorityEnd()
	{
		Behaviour[] array = componentEnabledOutsideFungusPriority;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
		array = componentEnabledInsideFungusPriority;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
	}

	private void FungusPrioritySignals_OnFungusPriorityStart()
	{
		Behaviour[] array = componentEnabledOutsideFungusPriority;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = false;
		}
		array = componentEnabledInsideFungusPriority;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].enabled = true;
		}
	}

	private void Update()
	{
	}
}
