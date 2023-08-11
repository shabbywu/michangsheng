using System;

namespace MarkerMetro.Unity.WinLegacy.Reflection;

[Flags]
public enum BindingFlags
{
	Default = 0,
	Public = 1,
	Instance = 2,
	InvokeMethod = 3,
	NonPublic = 4,
	Static = 5,
	FlattenHierarchy = 6,
	DeclaredOnly = 7,
	IgnoreCase = 8
}
