using UltimateSurvival.InputSystem;
using UnityEngine;

namespace UltimateSurvival;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private SurfaceDatabase m_SurfaceDatabase;

	[SerializeField]
	private ItemDatabase m_ItemDatabase;

	private static MGInputManager m_InputManager;

	private static PlayerEventHandler m_Player;

	public static PlayerEventHandler LocalPlayer
	{
		get
		{
			if ((Object)(object)m_Player == (Object)null)
			{
				m_Player = Object.FindObjectOfType<PlayerEventHandler>();
			}
			return m_Player;
		}
	}

	public static MGInputManager InputManager
	{
		get
		{
			if ((Object)(object)m_InputManager == (Object)null)
			{
				m_InputManager = Object.FindObjectOfType<MGInputManager>();
			}
			return m_InputManager;
		}
	}

	public static float NormalizedTime { get; set; }

	public static AudioUtils Audio { get; private set; }

	public static Camera WorldCamera { get; private set; }

	public static SurfaceDatabase SurfaceDatabase { get; private set; }

	public static ItemDatabase ItemDatabase { get; private set; }

	public static TreeManager TerrainHelpers { get; private set; }

	private void Awake()
	{
		Audio = ((Component)this).GetComponentInChildren<AudioUtils>();
		WorldCamera = ((Component)((Component)LocalPlayer).transform.FindDeepChild("World Camera")).GetComponent<Camera>();
		SurfaceDatabase = m_SurfaceDatabase;
		ItemDatabase = m_ItemDatabase;
		TerrainHelpers = ((Component)this).GetComponent<TreeManager>();
	}
}
