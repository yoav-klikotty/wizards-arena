using UnityEngine;
using UnityEngine.UI;
public class Magic : MonoBehaviour
{
    GameObject ToPoint;
    GameObject FromPoint;
    [SerializeField] GameObject _firePrefab;
    ParticleSystem _particalSystem;
    [SerializeField] Image _skillThumbnail;
    void Start()
    {
        if(transform.parent.gameObject.transform.parent.gameObject.name == "Opponent")
        {
            ToPoint = GameObject.Find("Player");
            FromPoint = GameObject.Find("CenterShootOpponent");
        }
        else
        {
            ToPoint = GameObject.Find("Opponent");
            FromPoint = GameObject.Find("CenterShootPlayer");
        }
        _particalSystem = GetComponent<ParticleSystem>();
    }
    public void Activate()
    {
        if (_firePrefab != null)
        {
            ActivateFirePrefab();
        }
        else
        {
            _particalSystem.Play();
            Invoke("Diactivate", 2);
        }

    }
    void ActivateFirePrefab()
    {
        GameObject instanceBullet = Instantiate(_firePrefab, FromPoint.transform.position, Quaternion.identity);
        instanceBullet.transform.rotation = Quaternion.LookRotation(ToPoint.transform.position - FromPoint.transform.position);
    }
    void Diactivate()
    {
        _particalSystem.Stop();
    }
    public Image GetThumbnail()
    {
        return _skillThumbnail;
    }
}
