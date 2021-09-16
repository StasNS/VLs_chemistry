using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Text _textField;
    [SerializeField] private List<Button> _button;
    private List<string> _textInfo = new List<string>();
    private int iterator = 0;

    #region TextReader
    private List<string> GetText(string File_Name)
    {
        TextAsset data = (TextAsset)Resources.Load(File_Name);
        string[] tmp = data.text.Split('\n');
        _textInfo.AddRange(tmp);
        return _textInfo;
    }
    #endregion
    #region TurnPages
    private void CheckEndOfText(int clickcount)
    {
        if (clickcount == _textInfo.Count + 1)
        {
            _textField.text = "";
            _textInfo.Clear();
            iterator = 0;
            ButtonActrivator();
        }
        else
        if (clickcount == -1 || clickcount == 0)
        {
            _textField.text = "";
            _textInfo.Clear();
            iterator = 0;
            ButtonActrivator();
        }
    }
    public void Next()
    {
        if (iterator != _textInfo.Count) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
        iterator++;
        CheckEndOfText(iterator);
    }
    public void Prev()
    {
        iterator--;
        if (iterator != _textInfo.Count) //костыль
        {
            _textField.text = _textInfo[iterator];
        }
        CheckEndOfText(iterator);
    }
    #endregion 
    #region MainButtons
    public void Vvodnii_But()
    {
        GetText("ControlText");
        ButtonActrivator();
        _textField.text = "Для перемещения используйте кнопки next и prev";
    }
    public void Metod_But()
    {
        GetText("MetodText");
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
