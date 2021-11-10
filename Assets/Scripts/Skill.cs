using UnityEngine;
using UnityEngine.UI;
public class Skill : MonoBehaviour
{
    public new string name;
    public Sprite sprite;
    public int damagePoints;
    public int defencePoints;
    public int requiredMana;
    ParticleSystem particalSystem;
    void Start(){
        particalSystem = GetComponent<ParticleSystem>();
    }
    public void Activate(){
        particalSystem.Play();
        Invoke("Diactivate", 2);
    }
    void Diactivate()
    {
        particalSystem.Stop();
    }
 }
