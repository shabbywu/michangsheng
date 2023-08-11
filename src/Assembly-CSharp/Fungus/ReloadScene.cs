using UnityEngine;
using UnityEngine.SceneManagement;

namespace Fungus;

[CommandInfo("Scene", "Reload", "Reload the current scene", 0)]
[AddComponentMenu("")]
public class ReloadScene : Command
{
	[Tooltip("Image to display while loading the scene")]
	[SerializeField]
	protected Texture2D loadingImage;

	public override void OnEnter()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		Scene activeScene = SceneManager.GetActiveScene();
		SceneLoader.LoadScene(((Scene)(ref activeScene)).name, loadingImage);
		Continue();
	}

	public override string GetSummary()
	{
		return "";
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}
}
