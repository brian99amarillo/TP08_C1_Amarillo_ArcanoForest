using UnityEngine;

public class PickableHealt : Pickable
{
    [SerializeField] private float healthValue = 100f;

    protected override bool Apply(GameObject player)
    {
        AudioManager.Instance.PickHealth();
        HealthPlayer health = player.GetComponentInParent<HealthPlayer>();
        if (player != null)
        {
           health.Heal(healthValue);
        }
        
        return true;
    }

}
