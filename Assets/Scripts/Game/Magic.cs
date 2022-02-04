using UnityEngine;
using UnityEngine.UI;
public class Magic : MonoBehaviour
{
    GameObject ToPoint;
    GameObject FromPoint;
    [SerializeField] GameObject _firePrefab;
    ParticleSystem _particalSystem;
    [SerializeField] Image _skillThumbnail;
    [SerializeField] BoxCollider _boxCollider;

    void Start()
    {
        _particalSystem = GetComponent<ParticleSystem>();
    }
    public void Activate()
    {
        _particalSystem.Play();
        if (_boxCollider != null)
        {
            EnableBoxCollider();
        }
    }

    private void EnableBoxCollider()
    {
        _boxCollider.enabled = true;
        Invoke("DisableBoxCollider", 1);
    }
    private void DisableBoxCollider()
    {
        _boxCollider.enabled = false;
    }
    public void ActivateFirePrefab(Vector3 FromPoint, Vector3 ToPoint)
    {
        GameObject instanceBullet = Instantiate(_firePrefab, FromPoint, Quaternion.identity);
        instanceBullet.transform.rotation = Quaternion.LookRotation(ToPoint - FromPoint);
    }
    public Image GetThumbnail()
    {
        return _skillThumbnail;
    }
}
