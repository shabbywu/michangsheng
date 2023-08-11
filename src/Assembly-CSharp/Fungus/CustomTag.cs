using System.Collections.Generic;
using UnityEngine;

namespace Fungus;

[ExecuteInEditMode]
public class CustomTag : MonoBehaviour
{
	[Tooltip("String that defines the start of the tag.")]
	[SerializeField]
	protected string tagStartSymbol;

	[Tooltip("String that defines the end of the tag.")]
	[SerializeField]
	protected string tagEndSymbol;

	[Tooltip("String to replace the start tag with.")]
	[SerializeField]
	protected string replaceTagStartWith;

	[Tooltip("String to replace the end tag with.")]
	[SerializeField]
	protected string replaceTagEndWith;

	public static List<CustomTag> activeCustomTags = new List<CustomTag>();

	public virtual string TagStartSymbol => tagStartSymbol;

	public virtual string TagEndSymbol => tagEndSymbol;

	public virtual string ReplaceTagStartWith => replaceTagStartWith;

	public virtual string ReplaceTagEndWith => replaceTagEndWith;

	protected virtual void OnEnable()
	{
		if (!activeCustomTags.Contains(this))
		{
			activeCustomTags.Add(this);
		}
	}

	protected virtual void OnDisable()
	{
		activeCustomTags.Remove(this);
	}
}
