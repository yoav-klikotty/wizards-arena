using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    public GameObject ToPoint;
    public GameObject FromPoint;
    ParticleSystem _particalSystem;
    [SerializeField] GameObject _firePrefab;
    void Start()
    {
        _particalSystem = GetComponent<ParticleSystem>();
    }
    public void Activate()
    {
        _particalSystem.Play();
        Invoke("Diactivate", 2);
    }
    public void ActivateFirePrefab()
    {
        GameObject instanceBullet = Instantiate(_firePrefab, FromPoint.transform.position, Quaternion.identity);
        instanceBullet.transform.rotation = Quaternion.LookRotation(ToPoint.transform.position - FromPoint.transform.position);
    }
    void Diactivate()
    {
        _particalSystem.Stop();
    }
}
