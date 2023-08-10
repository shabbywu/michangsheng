using System;
using UnityEngine;

namespace Fungus;

[CommandInfo("Flow", "Save Point", "Creates a Save Point and adds it to the Save History. The player can save the Save History to persistent storage and load it again later using the Save Menu.", 0)]
public class SavePoint : Command
{
	public enum KeyMode
	{
		BlockName,
		Custom,
		BlockNameAndCustom
	}

	public enum DescriptionMode
	{
		Timestamp,
		Custom
	}

	[Tooltip("Marks this Save Point as the starting point for Flowchart execution in the scene. Each scene in your game should have exactly one Save Point with this enabled.")]
	[SerializeField]
	protected bool isStartPoint;

	[Tooltip("How the Save Point Key for this Save Point is defined.")]
	[SerializeField]
	protected KeyMode keyMode;

	[Tooltip("A string key which uniquely identifies this save point.")]
	[SerializeField]
	protected string customKey = "";

	[Tooltip("A string to seperate the block name and custom key when using KeyMode.Both.")]
	[SerializeField]
	protected string keySeparator = "_";

	[Tooltip("How the description for this Save Point is defined.")]
	[SerializeField]
	protected DescriptionMode descriptionMode;

	[Tooltip("A short description of this save point.")]
	[SerializeField]
	protected string customDescription;

	[Tooltip("Fire a Save Point Loaded event when this command executes.")]
	[SerializeField]
	protected bool fireEvent = true;

	[Tooltip("Resume execution from this location after loading this Save Point.")]
	[SerializeField]
	protected bool resumeOnLoad = true;

	public bool IsStartPoint
	{
		get
		{
			return isStartPoint;
		}
		set
		{
			isStartPoint = value;
		}
	}

	public string SavePointKey
	{
		get
		{
			if (keyMode == KeyMode.BlockName)
			{
				return ParentBlock.BlockName;
			}
			if (keyMode == KeyMode.BlockNameAndCustom)
			{
				return ParentBlock.BlockName + keySeparator + customKey;
			}
			return customKey;
		}
	}

	public string SavePointDescription
	{
		get
		{
			if (descriptionMode == DescriptionMode.Timestamp)
			{
				return DateTime.UtcNow.ToString("HH:mm dd MMMM, yyyy");
			}
			return customDescription;
		}
	}

	public bool ResumeOnLoad => resumeOnLoad;

	public override void OnEnter()
	{
		FungusManager.Instance.SaveManager.AddSavePoint(SavePointKey, SavePointDescription);
		if (fireEvent)
		{
			SavePointLoaded.NotifyEventHandlers(SavePointKey);
		}
		Continue();
	}

	public override string GetSummary()
	{
		if (keyMode == KeyMode.BlockName)
		{
			return "key: " + ParentBlock.BlockName;
		}
		if (keyMode == KeyMode.BlockNameAndCustom)
		{
			return "key: " + ParentBlock.BlockName + keySeparator + customKey;
		}
		return "key: " + customKey;
	}

	public override Color GetButtonColor()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color32.op_Implicit(new Color32((byte)235, (byte)191, (byte)217, byte.MaxValue));
	}

	public override bool IsPropertyVisible(string propertyName)
	{
		if (propertyName == "customKey" && keyMode != KeyMode.Custom && keyMode != KeyMode.BlockNameAndCustom)
		{
			return false;
		}
		if (propertyName == "keySeparator" && keyMode != KeyMode.BlockNameAndCustom)
		{
			return false;
		}
		if (propertyName == "customDescription" && descriptionMode != DescriptionMode.Custom)
		{
			return false;
		}
		return true;
	}
}
