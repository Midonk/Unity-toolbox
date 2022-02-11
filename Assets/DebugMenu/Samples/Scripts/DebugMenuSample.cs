using UnityEngine;
using TF.DebugMenu.Attributes;

public class DebugMenuSample : MonoBehaviour
{
    #region Exposed

    [SerializeField]
    private string message;
            
    #endregion


    #region Main

    [DebugMenu("Game/Reset", true)]
    public void DebugMenuTestingMethod()
    {
        Debug.Log(message);
    }

    [DebugMenu("Level/Reload current")]
    public void ReloadLevel()
    {
        Debug.Log("Level reloading");
    }
    
    [DebugMenuToggle("Game/Auto save", false, true)]
    public void DebugMenuTestingMethodBool(bool value)
    {
        Debug.Log(value);
    }
    
    [DebugMenuInt("Game/Option/MSAA", 0, true)]
    public void DebugMenuTestingMethodInt(int value)
    {
        Debug.Log(value);
    }
    
    [DebugMenu("Player/Stats/Yei")]
    public void DebugMenuTestingMethod2()
    {
        Debug.Log(message);
    }
    
    [DebugMenu("Player/Stats/Stats", true)]
    public void DebugMenuTestingMethod3()
    {
        Debug.Log(message);
    }

    [DebugMenu("Player/Stats/Much/Machin")]
    public void DebugMenuTestingMethod4()
    {
        Debug.Log(message);
    }

    [DebugMenu("Player/Stats/Much/Bidule")]
    public void DebugMenuTestingMethod5()
    {
        Debug.Log(message);
    }
    
    [DebugMenuState("Game/Difficulty", typeof(Difficulty), (int)Difficulty.Harcore, false)]
    public void DebugMenuTestingEnum(Difficulty dessert)
    {
        Debug.Log(dessert.ToString());
    }
            
    #endregion
}

public enum Difficulty
{
    Peaceful,
    Normal,
    Hard,
    Harcore
}