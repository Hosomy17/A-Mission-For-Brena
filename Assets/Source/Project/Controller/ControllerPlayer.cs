using UnityEngine;
using System.Collections;

public class ControllerPlayer : ControllerGeneric
{
    private ClassPlayer classPlayer;

    public override void SendInput(System.Collections.Generic.Dictionary<string, object> input)
    {
        string name = (string)input["Name"];
        switch (name)
        {
            case "Directional":
                Move((Vector2)input["Axis"]);
                break;
            case "Fire1":
                Shoot((string)input["State"]);
                break;
        }
    }

    public void Shoot(string state)
    {
        if (state == "Hold" && !classPlayer.lockShoot)
            classPlayer.Shoot();
    }

    public override void TrackObject(GameObject gameObject)
    {
        classPlayer = gameObject.GetComponent<ClassPlayer>();
    }

    public void Move(Vector2 directions)
    {
        FacadePlayer.Move(classPlayer, directions, classPlayer.flySpeed);

        if (directions.x == 0)
        {
            FacadePlayer.Idle(classPlayer, Vector2.right);
        }
        if (directions.y == 0)
        {
            FacadePlayer.Idle(classPlayer, Vector2.up);
        }
    }
}
