using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ControllerGeneric
{
    public abstract void SendInput(Dictionary<string, object> input);

    public abstract void TrackObject(GameObject gameObject);
}