using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerChecker : MonoBehaviour {

    private static string[] connectedControllers = new string[0];

    //check if controller is connected
    private static bool ControllerConnected()
    {
        bool isConnected = false;

        //get all controllers
        connectedControllers = Input.GetJoystickNames();

        //check if array contains anything
        if(connectedControllers.Length > 0)
        {
            //for all entries
            for(int i = 0; i < connectedControllers.Length; i++)
            {
                //if string is not empty, then controller <- keyboard is empty
                if (!string.IsNullOrEmpty(connectedControllers[i]))
                {
                    //there is a controller connected
                    isConnected = true;
                }
            }
        }
        return isConnected;
    }
}
