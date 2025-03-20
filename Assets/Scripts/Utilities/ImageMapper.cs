using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageMapper : MonoBehaviour
{
    public static Sprite RetrieveHumanoidImageFromClass(HumanoidClass c)
    {
        switch (c)
        {
            case HumanoidClass.Warrior:
                return Resources.Load<Sprite>("Images/Warrior");

            case HumanoidClass.Mage:
                return Resources.Load<Sprite>("Images/Mage");

            case HumanoidClass.Archer:
                return Resources.Load<Sprite>("Images/Archer");

            default:
                return null;
        }

    }
}
