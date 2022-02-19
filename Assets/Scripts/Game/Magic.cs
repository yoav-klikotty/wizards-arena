using UnityEngine;
using UnityEngine.UI;
public class Magic : MonoBehaviour
{
    GameObject ToPoint;
    GameObject FromPoint;
    [SerializeField] GameObject _firePrefab;
    ParticleSystem _particalSystem;
    [SerializeField] Sprite _skillThumbnail;
    [SerializeField] BoxCollider _boxCollider;
    [SerializeField] int _requiredMana;
    [SerializeField] MagicType _magicType;
    public enum MagicType { Attack, Mana, Defence };
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

    public int GetRequiredMana()
    {
        return _requiredMana;
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
        instanceBullet.GetComponent<ProjectileMover>().wizardStats = gameObject.GetComponentInParent<Wizard>().WizardStatsData;
        instanceBullet.transform.rotation = Quaternion.LookRotation(ToPoint - FromPoint);
    }
    public Sprite GetThumbnail()
    {
        return _skillThumbnail;
    }
    public MagicType GetMagicType()
    {
        return _magicType;
    }
}
