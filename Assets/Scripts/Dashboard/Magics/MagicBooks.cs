using UnityEngine;
using UnityEngine.SceneManagement;

public class MagicBooks : MonoBehaviour
{
    [SerializeField] GameObject _attackBook;
    [SerializeField] GameObject _defenceBook;
    [SerializeField] GameObject _manaBook;

    public void ChooseAttackBook()
    {
        _attackBook.SetActive(true);
        _defenceBook.SetActive(false);
        _manaBook.SetActive(false);
    }
    public void ChooseDefenceBook()
    {
        _attackBook.SetActive(false);
        _defenceBook.SetActive(true);
        _manaBook.SetActive(false);
    }
    public void ChooseManaBook()
    {
        _attackBook.SetActive(false);
        _defenceBook.SetActive(false);
        _manaBook.SetActive(true);
    }

    public MagicBook GetCurrentBook()
    {
        if (_attackBook.activeSelf)
        {
            return _attackBook.GetComponent<MagicBook>();
        }
        else if (_defenceBook.activeSelf)
        {
            return _defenceBook.GetComponent<MagicBook>();
        }
        else
        {
            return _manaBook.GetComponent<MagicBook>();
        }
    }
    public void GoToDashboard()
    {
        SceneManager.LoadScene("Dashboard");
    }
}
