using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour {

	public const string IDLE	 = "Wizard_Idle";
	public const string RUN		 = "Wizard_Run";
	public const string ATTACK	 = "Wizard_Attack";
	public const string SKILL	 = "Wizard_Skill";
	public const string DAMAGE	 = "Wizard_Damage";
	public const string STUN	 = "Wizard_Stun";
	public const string DEATH	 = "Wizard_Death";
    public Player Player;
	Animation _anim;

    private bool _isDeadAnimationPlayed = false;

	void Start () {
		_anim = GetComponent<Animation>();
	}

    void Update()
    {
       
    }

	public void IdleAni (){
		_anim.CrossFade (IDLE);
	}

	public void RunAni (){
		_anim.CrossFade (RUN);
	}

	public void AttackAni (){
		_anim.CrossFade (ATTACK);
	}

	public void SkillAni (){
		_anim.CrossFade (SKILL);
	}

	public void DamageAni (){
		_anim.CrossFade (DAMAGE);
	}

	public void StunAni (){
		_anim.CrossFade (STUN);
	}

	public void DeathAni (){
		_anim.CrossFade (DEATH);
	}
    void OnCollisionEnter(Collision collision)
    {
        DamageAni();    
        if (Player.GetHealth() <= 0)
        {
            DeathAni();
        }
    }

	public void StopAni (){
		_anim.Stop();
	}
		
}