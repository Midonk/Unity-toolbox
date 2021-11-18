using UnityEngine;

namespace ScriptableVariables
{
    [CreateAssetMenu(fileName="NewObservableFloatVariable", menuName="Scriptable/Observable/FloatVariable")]
    public class ObservableFloatVariable : ObservableVariable<float>
    {}
}