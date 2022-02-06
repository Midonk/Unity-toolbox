using UnityEngine;

public class MenuBuilder : MonoBehaviour
{
    /*
    * This script is a facade
    * - It intends to process a bunch of data to build a UI
    * - To process those inputs, the builder will build a kind of big swith that check a certain rule. 
        If that rule is validated, that associated link (processor) will process the data
    * - The processor will output a UI and returns it to the builder to be handled by a LayoutHandler subsystem
    */
}