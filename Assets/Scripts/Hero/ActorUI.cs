using UnityEngine;

public class ActorUI : MonoBehaviour
{
    public HpBar HpBar;

    public HeroHealth _heroHealth;
    private void OnDestroy() => 
        _heroHealth.HeroHealthChange -= UpdateHpBar;

    public void Construct(HeroHealth heroHealth)
    {
        _heroHealth = heroHealth;

        _heroHealth.HeroHealthChange += UpdateHpBar;
    }

    private void UpdateHpBar() => 
        HpBar.SetValue(_heroHealth.CurrentHp, _heroHealth.MaxHp);

}
