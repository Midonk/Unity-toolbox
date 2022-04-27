using UnityEngine;
using TF.Tool;

public class UnusedAssetToolSettings : ToolSettings<UnusedAssetToolSettings>
{
    [Header("Excludes")]
    [SerializeField] private string _excludeStarting;
    [SerializeField] private string _excludeContaining;
    [SerializeField] private string _excludeEnding;

    public string[] StartExcludes => _excludeStarting.Split(';');
    public string[] ContainExcludes => _excludeContaining.Split(';');
    public string[] EndExcludes => _excludeEnding.Split(';');

    protected override void Initialize()
    {
        _excludeStarting = "";
        _excludeContaining = "";
        _excludeEnding = ".cs;.pdf;.php;.json;.asmdef;.md";
    }
}

//Assets/AddressableAssetsData;Assets/Editor/Settings/