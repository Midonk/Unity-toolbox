using UnityEngine;
using TF.Tool;

[CreateAssetMenu(menuName = "Settings/Shape builder", fileName = "New shape builder settings")]
public class ShapeBuilderSettings : ToolSettings<ShapeBuilderSettings>
{
    [SerializeField] private EditorInputTrigger _inputTrigger;
    [SerializeField] private ShapeBuilder _builder = new ShapeBuilder();

    public EditorInputTrigger InputTrigger => _inputTrigger;
    public ShapeBuilder Builder => _builder;
}