using UnityEngine;

namespace UltimateSurvival.GUISystem;

public class GUIBehaviour : MonoBehaviour
{
	private GUIController m_Controller;

	public GUIController Controller
	{
		get
		{
			if ((Object)(object)m_Controller == (Object)null)
			{
				m_Controller = ((Component)this).GetComponentInParent<GUIController>();
			}
			return m_Controller;
		}
	}

	public PlayerEventHandler Player
	{
		get
		{
			if (!Object.op_Implicit((Object)(object)Controller))
			{
				return null;
			}
			return Controller.Player;
		}
	}
}
