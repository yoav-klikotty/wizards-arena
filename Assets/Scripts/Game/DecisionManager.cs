using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour
{
    [SerializeField] Counter _counter;
    [SerializeField] Option _option;
    Syncronizer _syncronizer;
    [SerializeField] Button _shootBtn;
    [SerializeField] Button _ammoBtn;
    [SerializeField] Button _shieldBtn;
    Player player;
    public enum Option
    {
        Reload,
        Protect,
        Shoot,
        None
    }

    void Start()
    {
        _syncronizer = GameObject.Find("Syncronizer").GetComponent<Syncronizer>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player.GetManaBar() > 0)
        {
            _shootBtn.interactable = true;
        }
        else
        {
            _shootBtn.interactable = false;
        }
    }
    void Update()
    {
        if (_counter.IsCounterEnd() && _option == Option.None)
        {
            ChooseRandom();
        }
    }

    public Option GetOption()
    {

        return _option;

    }

    public void ChooseAmmo()
    {

        if (!IsDecisionMakingOver())
        {
            _syncronizer.UpdatePlayersDecision(Option.Reload);
            _option = Option.Reload;
            _shootBtn.interactable = false;
            _shieldBtn.interactable = false;
        }

    }

    public void ChooseShield()
    {

        if (!IsDecisionMakingOver())
        {
            _syncronizer.UpdatePlayersDecision(Option.Protect);
            _option = Option.Protect;
            _ammoBtn.interactable = false;
            _shootBtn.interactable = false;
        }

    }

    public void ChooseShoot()
    {

        if (!IsDecisionMakingOver())
        {
            _syncronizer.UpdatePlayersDecision(Option.Shoot);
            _option = Option.Shoot;
            _ammoBtn.interactable = false;
            _shieldBtn.interactable = false;
        }
    }

    public void ChooseRandom()
    {

        _syncronizer.UpdatePlayersDecision(Option.Reload);
        _option = Option.Reload;

    }

    public bool IsDecisionMakingOver()
    {

        return _option != Option.None;

    }

}
