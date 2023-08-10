using UnityEngine;

namespace Fungus;

[RequireComponent(typeof(CameraManager))]
[RequireComponent(typeof(MusicManager))]
[RequireComponent(typeof(EventDispatcher))]
[RequireComponent(typeof(GlobalVariables))]
[RequireComponent(typeof(SaveManager))]
[RequireComponent(typeof(NarrativeLog))]
public sealed class FungusManager : MonoBehaviour
{
	private static FungusManager instance;

	private static bool applicationIsQuitting = false;

	public Flowchart jieShaBlock;

	private static object _lock = new object();

	public CameraManager CameraManager { get; private set; }

	public MusicManager MusicManager { get; private set; }

	public EventDispatcher EventDispatcher { get; private set; }

	public GlobalVariables GlobalVariables { get; private set; }

	public SaveManager SaveManager { get; private set; }

	public NarrativeLog NarrativeLog { get; private set; }

	public static FungusManager Instance
	{
		get
		{
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Expected O, but got Unknown
			if (applicationIsQuitting)
			{
				Debug.LogWarning((object)"FungusManager.Instance() was called while application is quitting. Returning null instead.");
				return null;
			}
			lock (_lock)
			{
				if ((Object)(object)instance == (Object)null)
				{
					GameObject val = new GameObject
					{
						name = "FungusManager"
					};
					Object.DontDestroyOnLoad((Object)val);
					instance = val.AddComponent<FungusManager>();
				}
				return instance;
			}
		}
	}

	private void Awake()
	{
		CameraManager = ((Component)this).GetComponent<CameraManager>();
		MusicManager = ((Component)this).GetComponent<MusicManager>();
		EventDispatcher = ((Component)this).GetComponent<EventDispatcher>();
		GlobalVariables = ((Component)this).GetComponent<GlobalVariables>();
		SaveManager = ((Component)this).GetComponent<SaveManager>();
		NarrativeLog = ((Component)this).GetComponent<NarrativeLog>();
	}

	private void OnDestroy()
	{
		applicationIsQuitting = true;
	}
}
