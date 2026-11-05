using UnityEngine;

public class PickableInvincibility : Pickable
{
    [SerializeField] private float duration = 5f;

    protected override bool Apply(GameObject player)
    {
        AudioManager.Instance.PickInvincibility();
        HealthPlayer health = player.GetComponentInParent<HealthPlayer>();
        if (health == null) return false;

        health.SetInvulnerable(duration);
        return true;
    }
}
