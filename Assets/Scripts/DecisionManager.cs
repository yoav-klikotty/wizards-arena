using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter counter;
    [SerializeField] Option option;
    Player playerHUD;
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
        playerHUD = GameObject.Find("PlayerHUD").GetComponent<Player>();
        playerInventory = playerHUD.transform.Find("Inventory").GetComponent<Inventory>();
        syncronizer = GameObject.Find("Syncronizer").GetComponent<Syncronizer>();
        Image shootBtnSprite = shootBtn.gameObject.GetComponent<Image>();
        shootBtnSprite.sprite = playerInventory.GetCurrentWeapon().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (counter.IsCounterEnd() && option == Option.None)
        {
            ChooseRandom();
        }
        if (playerHUD.GetAmmo() == 0){
            shootBtn.interactable = false;
        }
        else{
            shootBtn.interactable = true;
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
