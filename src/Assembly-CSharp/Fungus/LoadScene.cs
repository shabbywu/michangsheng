using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Flow", "Load Scene", "Loads a new Unity scene and displays an optional loading image. This is useful for splitting a large game across multiple scene files to reduce peak memory usage. Previously loaded assets will be released before loading the scene to free up memory.The scene to be loaded must be added to the scene list in Build Settings.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class LoadScene : Command
{
	[Tooltip("Name of the scene to load. The scene must also be added to the build settings.")]
	[SerializeField]
	protected StringData _sceneName = new StringData("");

	[Tooltip("Image to display while loading the scene")]
	[SerializeField]
	protected Texture2D loadingImage;

	[Tooltip("是否重新設置返回的上一個場景為當前設置的值")]
	[SerializeField]
	protected bool ResetLastScene = true;

	[HideInInspector]
	[FormerlySerializedAs("sceneName")]
	public string sceneNameOLD = "";

	public override void OnEnter()
	{
		Tools.instance.getPlayer().zulinContorl.kezhanLastScence = Tools.getScreenName();
		if (_sceneName.Value == "LianDan")
		{
			PanelMamager.inst.OpenPanel(PanelMamager.PanelType.炼丹);
		}
		else
		{
			Tools.instance.loadMapScenes(_sceneName.Value, ResetLastScene);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (_sceneName.Value.Length == 0)
		{
			return "Error: No scene name selected";
		}
		return _sceneName.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		if (!((Object)(object)_sceneName.stringRef == (Object)(object)variable))
		{
			return base.HasReference(variable);
		}
		return true;
	}

	protected virtual void OnEnable()
	{
		if (sceneNameOLD != "")
		{
			_sceneName.Value = sceneNameOLD;
			sceneNameOLD = "";
		}
	}
}
