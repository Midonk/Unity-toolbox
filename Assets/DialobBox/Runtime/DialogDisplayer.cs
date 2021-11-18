using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

/*
* Handles text display into Text UI
*/

public class DialogDisplayer : MonoBehaviour
{
    [SerializeField]
    private Text _textBox;


    #region Events

    public event System.Action m_onDisplaySuspend;
    public event System.Action m_onDisplayResume;
         
    #endregion
    

    #region Main

    public void DisplayText()
    {
        if(!_isDisplaying)
        {
            if(!DialogEnded())
            {
                StartCoroutine(SendLines());
            }

            else
            {
                ClearDialogBox();
            }
        }

        else
        {
            SetDisplayMode(_skipMode);
        }
    }

    public void DisplayText(string textToDisplay)
    {
        _charToDisplay.Clear();
        SplitText(textToDisplay);
        ResetDisplayMode();

        ClearDialogBox();
        StartCoroutine(SendLines());
    }

    public void ResetDisplayMode()
    {
        SetDisplayMode(_skipMode);
    }
         
    #endregion


    #region Plumbery

    protected virtual void ClearDialogBox()
    {
        _textBox.text = "";
    }

    private void SetDisplayMode(IDialogDisplayMode mode)
    {
        _textDisplayRate = 1 / mode.DisplayTextPortionPerSecond;
    }
         
    #endregion


    #region Coroutine

    protected virtual IEnumerator SendLines()
    {
        ClearDialogBox();
        m_onDisplayResume?.Invoke();

        while (_charToDisplay.Count > 0 /*special parsed characters ?*/)
        {
            int elementToDisplay = Mathf.FloorToInt(Time.deltaTime / _textDisplayRate);
            string textToDisplay = "";

            for (int i = 0; i < elementToDisplay; i++)
            {
                textToDisplay += _currentDisplayMode.GetNextTextToDisplay(_charToDisplay);
            }

            yield return null;
        }

        m_onDisplaySuspend?.Invoke();
    }
         
    #endregion


    #region Utils

    public virtual bool DialogEnded()
    {
        return _charToDisplay.Count == 0;
    }

    //might depend on the character ? A split method ?
    protected virtual void SplitText(string textToSplit)
    {
        foreach (var character in textToSplit)
        {
            _charToDisplay.Enqueue(character);
        }
    }
         
    #endregion

    
    #region Private Fields

    private bool _isDisplaying;
    private Queue<char> _charToDisplay;
    private IDialogDisplayMode _currentDisplayMode;
    private float _textDisplayRate;

    //setup
    private IDialogDisplayMode _primaryMode;
    private IDialogDisplayMode _skipMode;

    #endregion
}

//quand on appuie sur une touche pour accélérer / passer un dialogue, il y a consultation de la méthode de skip
//quand on relache le bouton de skip, retour à la méthode d'affichage classique 