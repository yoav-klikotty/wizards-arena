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
    [SerializeField] int _requiredHp;
    public bool AOE;
    [SerializeField] MagicType _magicType;
    public DefenceStatsData DefenceStatsData;
    public AttackStatsData AttackStatsData;
    public ManaStatsData ManaStatsData;
    public enum MagicType { Attack, Mana, Defence };
    [SerializeField] string _pattern;
    [SerializeField] AudioClip sound;
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

    public int GetRequiredHp()
    {
        return _requiredHp;
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
        instanceBullet.GetComponent<ProjectileMover>().Attacker = gameObject.GetComponentInParent<Wizard>();
        instanceBullet.GetComponent<ProjectileMover>().MagicAttackStatsData = AttackStatsData;
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
    void OnCollisionEnter(Collision collision)
    {
        var attacker = collision.gameObject.GetComponent<ProjectileMover>().Attacker;
        var attackerMagic = collision.gameObject.GetComponent<ProjectileMover>().MagicAttackStatsData;
        gameObject.transform.parent.GetComponent<Wizard>().OnShieldCollision(attacker, attackerMagic, DefenceStatsData.HP);
    }
    public string GetPattern()
    {
        return _pattern;
    }

    public AudioClip getSound()
    {
        return sound;
    }
}
