using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementOverhead : MonoBehaviour
{
    // Many Scripts use the Element Enum, so this script exists to allow
    // them all to communicate with each other rather than have several different Enums

    public enum Element {Light, Dark, Gray};

    // Same cas for the Team Enum
    public enum Team { Player, Enemy };
}
