using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public enum HumanoidClass
{ 
    [Description("Enemy")]
    Enemy,

    [Description("Warrior")]
    Warrior,

    [Description("Mage")]
    Mage,

    [Description("Archer")]
    Archer,
}