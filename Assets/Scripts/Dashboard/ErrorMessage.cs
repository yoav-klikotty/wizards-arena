using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ErrorMessage : MonoBehaviour
{
    [SerializeField] TMP_Text _messaeg;

    public void SetMessage(string message)
    {
        _messaeg.text = "Oops! " + message;
    }

    public void DeleteMessage()
    {
        _messaeg.text = "";
    }

}
