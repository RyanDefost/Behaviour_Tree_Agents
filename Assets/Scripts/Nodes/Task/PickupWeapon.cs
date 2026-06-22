using UnityEngine;

public class PickupWeapon : TaskNode
{
    string BBWeapon;
    Vector3 weapon;

    public PickupWeapon(Agent agent, string BBWeapon) : base(agent)
    {
        this.BBWeapon = BBWeapon;
    }

    public override void OnEnter()
    {
        this.weapon = Blackboard.GetValue<Vector3>(this.BBWeapon);
    }

    public override Status OnUpdate()
    {
        Debug.LogWarning("PICKING UP!!!@");
        this.Blackboard.SetValue("HASWEAPON", true);
        //this.weapon.gameObject.SetActive(false);

        return Status.SUCCESS;
    }
}
