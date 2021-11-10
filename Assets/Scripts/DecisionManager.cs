using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter counter;
    [SerializeField] Option option;
    Syncronizer syncronizer;
    [SerializeField] Button shootBtn;
    [SerializeField] Button ammoBtn;
    [SerializeField] Button shieldBtn;
    Inventory playerInventory;
    public enum Option
    {
        Reload,
        Protect,
        Shoot,
        None
    }

    void Start()
    {
        syncronizer = GameObject.Find("Syncronizer").GetComponent<Syncronizer>();
        Image shootBtnSprite = shootBtn.gameObject.GetComponent<Image>();
    }
    void Update()
    {
        if (counter.IsCounterEnd() && option == Option.None)
        {
            ChooseRandom();
        }
    }

    public Option GetOption()
    {

        return option;

    }

    public void ChooseAmmo()
    {

        if (!IsDecisionMakingOver())
        {
            syncronizer.UpdatePlayersDecision(Option.Reload);
            option = Option.Reload;
            shootBtn.interactable = false;
            shieldBtn.interactable = false;
        }

    }

    public void ChooseShield()
    {

        if (!IsDecisionMakingOver())
        {
            syncronizer.UpdatePlayersDecision(Option.Protect);
            option = Option.Protect;
            ammoBtn.interactable = false;
            shootBtn.interactable = false;
        }

    }

    public void ChooseShoot()
    {

        if (!IsDecisionMakingOver())
        {
            syncronizer.UpdatePlayersDecision(Option.Shoot);
            option = Option.Shoot;
            ammoBtn.interactable = false;
            shieldBtn.interactable = false;
        }
    }

    public void ChooseRandom()
    {

        syncronizer.UpdatePlayersDecision(Option.Reload);
        option = Option.Reload;

    }

    public bool IsDecisionMakingOver()
    {

        return option != Option.None;

    }

}
