using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    public GameObject ToPoint;
    public GameObject FromPoint;
    public new string name;
    public Sprite sprite;
    public int damagePoints;
    public int defencePoints;
    public int requiredMana;
    ParticleSystem particalSystem;
    public GameObject FirePrefab;
    void Start()
    {
        particalSystem = GetComponent<ParticleSystem>();
    }
    public void Activate()
    {
        particalSystem.Play();
        Invoke("Diactivate", 2);
    }
    public void ActivateFirePrefab()
    {
        GameObject instanceBullet = Instantiate(FirePrefab, FromPoint.transform.position, Quaternion.identity);
        instanceBullet.transform.rotation = Quaternion.LookRotation(ToPoint.transform.position - FromPoint.transform.position);
    }
    void Diactivate()
    {
        particalSystem.Stop();
    }
}
