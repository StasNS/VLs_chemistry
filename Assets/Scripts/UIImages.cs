using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImages : MonoBehaviour
{
    [SerializeField] private GameObject _imageField;
    [SerializeField] private List<Button> _button;
    private List<Sprite> _imageInfo = new List<Sprite>();
    private int iterator = -1;

    #region TurnPages
    private void CheckEndOfText(int clickcount)
    {
        if (clickcount == _imageInfo.Count)
        {
            _imageField.SetActive(false);
            _imageInfo.Clear();
            iterator = -1;
            ButtonActrivator();
        }
        if (clickcount == -1)
        {
            _imageField.SetActive(false);
            _imageInfo.Clear();
            iterator = -1;
            ButtonActrivator();
        }
    }
    public void Next()
    {
        iterator++;
        if (iterator != _imageInfo.Count) //костыль
        {
            _imageField.GetComponent<Image>().sprite  = _imageInfo[iterator];
        }
        CheckEndOfText(iterator);
    }
    public void Prev()
    {
        iterator--;
        if (iterator != _imageInfo.Count && iterator >= 0) //костыль
        {
            _imageField.GetComponent<Image>().sprite = _imageInfo[iterator];
        }
        else
            CheckEndOfText(iterator);
    }
    #endregion 
    public void Metod_but()
    {
        _imageField.SetActive(true);
        ButtonActrivator();
        Next();
    }
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
