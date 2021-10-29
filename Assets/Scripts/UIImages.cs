using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIImages : MonoBehaviour
{
    [SerializeField] private GameObject _imageField;
    [SerializeField] private List<Button> _button;
    private List<Sprite> _imageInfo = new List<Sprite>();
    private int iterator = -1;

    private List<Sprite> GetImages(string file_path)
    {
        var sprites = Resources.LoadAll(file_path,typeof(Sprite));    
        foreach (var image in sprites)
        {
            _imageInfo.Add((Sprite)image);
        }
        return _imageInfo;
    }
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
            _imageField.GetComponent<Image>().sprite = _imageInfo[iterator];
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
        GetImages("Chemical_Current");
        _imageField.SetActive(true);
        ButtonActrivator();
        Next();
    }
    public void Vvodnii_but()
    {
        GetImages("BasicControlls");
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
