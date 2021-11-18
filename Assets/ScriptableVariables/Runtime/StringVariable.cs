using UnityEngine;

namespace ScriptableVariables
{
	[CreateAssetMenu(fileName="NewStringVariable", menuName="Scriptable/StringVariable")]
	public class StringVariable : ScriptableVariable<string>{}
}