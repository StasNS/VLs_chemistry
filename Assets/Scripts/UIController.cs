using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private List<Button> _button;
    private List<string> _textInfo = new List<string>();
    private int iterator = 0;

    #region Mono

    private void Start()
    {
        GetText();
    }
    #endregion
    #region TextReader
    private List<string> GetText()
    {
        TextAsset data = (TextAsset)Resources.Load("ControlText");
        string[] tmp = data.text.Split('\n');
        _textInfo.AddRange(tmp);
        return _textInfo;
    }
    #endregion
    #region TurnPages
    private void CheckEndOfText(int clickcount)
    {
        if (clickcount == _textInfo.Count)
        {
            _textField.text = "";
            iterator = 0;
            ButtonActrivator();
        }
        else
            if (clickcount == -1 || clickcount == 0)
        {
            _textField.text = "";
            iterator = 0;
            ButtonActrivator();
        }
    }
    public void Next()
    {
        iterator++;
        if (iterator != _textInfo.Count) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
        CheckEndOfText(iterator);
    }
    public void Prev()
    {
        iterator--;
        CheckEndOfText(iterator);
        if (iterator > _textInfo.Count) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
    }
    #endregion 
    #region MainButtons
    public void ShowControl()
    {
        ButtonActrivator();
        _textField.text = "Для перемещения используйте кнопки next и prev";
    }
    #endregion
    private void ButtonActrivator()
    {
        foreach (var buttons in _button)
        {
            if (buttons.IsActive())
            {
                buttons.gameObject.SetActive(false);
            }
            else
            {
                buttons.gameObject.SetActive(true);
            }
        }
    }
}
