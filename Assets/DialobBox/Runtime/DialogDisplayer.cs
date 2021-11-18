using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

/*
* Handles text display into Text UI
* Receive string
* Display text following a display mode
*/

//don't like to be a monobehaviour
public class DialogDisplayer : MonoBehaviour
{
    #region Events

    public event System.Action m_onDisplayStart;
    public event System.Action m_onDisplayStop;
         
    #endregion


    #region Public Properties

    public bool IsDisplaying => _displayMode.IsDisplaying(_textToDisplay, _lastDisplayedIndex);
         
    #endregion
    

    #region Main

    public void DisplayText(string textToDisplay)
    {
        _textToDisplay = textToDisplay;
        StopAllCoroutines();
        StartCoroutine(SendLines());
    }
         
    public void SetDisplaySpeed(float elementPerSecond)
    {
        _displaySpeed = elementPerSecond;
    }
         
    #endregion


    #region Coroutine

    protected virtual IEnumerator SendLines()
    {
        m_onDisplayStart?.Invoke();

        float displayTime = 0;

        while (IsDisplaying)
        {
            displayTime += Time.deltaTime * _displaySpeed;
            _lastDisplayedIndex = Mathf.FloorToInt(displayTime);
            _lastDisplayedIndex = Mathf.Clamp(_lastDisplayedIndex, 0, _textToDisplay.Length);

            _textBox.text = _displayMode.GetText(_textToDisplay, _lastDisplayedIndex);

            yield return null;
        }

        m_onDisplayStop?.Invoke();
    }
         
    #endregion

    
    #region Private Fields

    private Text _textBox;
    private string _textToDisplay;
    private IDialogDisplayMode _currentDisplayMode;
    private float _displaySpeed;
    private int _lastDisplayedIndex = 0;

    //setup
    private IDialogDisplayMode _displayMode;

    #endregion
}

//quand on appuie sur une touche pour accélérer / passer un dialogue, il y a consultation de la méthode de skip
//quand on relache le bouton de skip, retour à la méthode d'affichage classique 