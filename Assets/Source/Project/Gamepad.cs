using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gamepad : GamepadGeneric
{
    public Dictionary<string,object> inputs;
    public ControllerGeneric controller;

    void Awake()
    {
        inputs = new Dictionary<string, object>();
        inputs.Add("Name" , "NULL"   );
        inputs.Add("State", "NULL"   );
        inputs.Add("Axis" , GetAxis());
	}

	void Update ()
    {
        inputs.Clear();
		inputs["Axis"] = GetAxis();
        for (int i = 1; i <= 2; i++)
        {
            inputs["Name"] = "Fire" + i;

            if (Input.GetButtonDown("Fire" + i)) inputs["State"] = "Down";
            else if (Input.GetButtonUp("Fire" + i)) inputs["State"] = "Up";
            else if (Input.GetButton("Fire" + i)) inputs["State"] = "Hold";
            else continue;
            controller.SendInput(inputs);
        }

        inputs["Name"] = "Directional";
        controller.SendInput(inputs);
	}

    private Vector2 GetAxis()
    {
        Vector2 axis = new Vector2();

        axis.x = Input.GetAxisRaw("Horizontal");
        axis.y = Input.GetAxisRaw("Vertical");

        return axis;
    }

	public void ButtonPressed(string button){
		Dictionary <string, object> command = new Dictionary<string, object>();
		command.Add ("ButtonPressed", button);
        controller.SendInput(command);
	}
}
