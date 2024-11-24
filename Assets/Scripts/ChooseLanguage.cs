using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLanguage : MonoBehaviour
{
    [SerializeField] public string[] myLangs;
    private int _id;
    [SerializeField] private Text buttonText;
    
    void Start()
    {
        _id = PlayerPrefs.GetInt ("_language_index", 0);
        PlayerPrefs.SetString("_language", myLangs[_id]);
        
        buttonText.text = _id == 0 ? "English" : _id == 1 ? "Русский" : "Portugues";
    }

    public void Choose()
    {
        _id++;
        if (_id == 1)
        {
            buttonText.text = "RUS";
            PlayerPrefs.SetInt("_language_index", _id);
            PlayerPrefs.SetString("_language", myLangs[_id]);
            Debug.Log ("language changed to " + myLangs [_id]);
        }

        if (_id == 2)
        {
            buttonText.text = "PORT";
            PlayerPrefs.SetInt("_language_index", _id);
            PlayerPrefs.SetString("_language", myLangs[_id]);
            Debug.Log ("language changed to " + myLangs [_id]);
        }
            
        if (_id > 2 || _id == 0)
        {
            _id = 0;
            buttonText.text = "ENG";
            PlayerPrefs.SetInt("_language_index", _id);
            PlayerPrefs.SetString("_language", myLangs[_id]);
            Debug.Log ("language changed to " + myLangs [_id]);
        }
        
        ApplyLanguageChanges();
    }
    void ApplyLanguageChanges ()
    {
        SceneManager.LoadScene (0);
    }
}
