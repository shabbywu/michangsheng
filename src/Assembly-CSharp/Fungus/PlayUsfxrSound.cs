using UnityEngine;
using UnityEngine.Serialization;

namespace Fungus;

[CommandInfo("Audio", "Play Usfxr Sound", "Plays a usfxr synth sound. Use the usfxr editor [Tools > Fungus > Utilities > Generate usfxr Sound Effects] to create the SettingsString. Set a ParentTransform if using positional sound. See https://github.com/zeh/usfxr for more information about usfxr.", 0)]
[AddComponentMenu("")]
[ExecuteInEditMode]
public class PlayUsfxrSound : Command
{
	[Tooltip("Transform to use for positional audio")]
	[SerializeField]
	protected Transform ParentTransform;

	[Tooltip("Settings string which describes the audio")]
	[SerializeField]
	protected StringDataMulti _SettingsString = new StringDataMulti("");

	[Tooltip("Time to wait before executing the next command")]
	[SerializeField]
	protected float waitDuration;

	protected SfxrSynth _synth = new SfxrSynth();

	[HideInInspector]
	[FormerlySerializedAs("SettingsString")]
	public string SettingsStringOLD = "";

	protected virtual void UpdateCache()
	{
		if (_SettingsString.Value != null)
		{
			_synth.parameters.SetSettingsString(_SettingsString.Value);
			_synth.CacheSound();
		}
	}

	protected virtual void Awake()
	{
		UpdateCache();
	}

	protected void DoWait()
	{
		Continue();
	}

	public override void OnEnter()
	{
		_synth.SetParentTransform(ParentTransform);
		_synth.Play();
		if (Mathf.Approximately(waitDuration, 0f))
		{
			Continue();
		}
		else
		{
			((MonoBehaviour)this).Invoke("DoWait", waitDuration);
		}
	}

	public override string GetSummary()
	{
		if (string.IsNullOrEmpty(_SettingsString.Value))
		{
			return "Settings String hasn't been set!";
		}
		if ((Object)(object)ParentTransform != (Object)null)
		{
			return ((Object)ParentTransform).name + ": " + _SettingsString.Value;
		}
		return "Camera.main: " + _SettingsString.Value;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)128, (byte)200, (byte)200, byte.MaxValue));
	}

	public override bool HasReference(Variable variable)
	{
		return (Object)(object)variable == (Object)(object)_SettingsString.stringRef;
	}

	protected virtual void OnEnable()
	{
		if (SettingsStringOLD != "")
		{
			_SettingsString.Value = SettingsStringOLD;
			SettingsStringOLD = "";
		}
	}
}
