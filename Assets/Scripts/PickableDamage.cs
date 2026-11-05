using UnityEngine;

public class PickableDamage: Pickable
{

    [SerializeField] private float extraDamage = 5f;
    [SerializeField] private float duration = 5f;

    protected override bool Apply(GameObject player)
    {   
        AudioManager.Instance.PickDamage(); // Reproduce el sonido de recoger el pickable de daño
        PlayerAttack attack = player.GetComponentInParent<PlayerAttack>();
        if (attack == null) return false;

        attack.ActivateExtraDamage(extraDamage, duration);
        return true;
    }

}