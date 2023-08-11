using UltimateSurvival.StandardAssets;
using UnityEngine;

namespace UltimateSurvival;

public class PlayerPauseHandler : MonoBehaviour
{
	[SerializeField]
	private DOF m_DOF;

	[SerializeField]
	private ColorCorrection m_ColorCorrectionCurves;

	private void Start()
	{
		MonoSingleton<InventoryController>.Instance.State.AddChangeListener(OnInventoryToggled);
	}

	private void OnInventoryToggled()
	{
		bool isClosed = MonoSingleton<InventoryController>.Instance.IsClosed;
		if (Object.op_Implicit((Object)(object)m_DOF))
		{
			((Behaviour)m_DOF).enabled = !isClosed;
		}
		if (Object.op_Implicit((Object)(object)m_ColorCorrectionCurves))
		{
			((Behaviour)m_ColorCorrectionCurves).enabled = !isClosed;
		}
	}
}
