using UnityEngine;
using TMPro;

public class PowerManager : MonoBehaviour
{
    public TMP_Text powerText;
    private int power;
    public int startingPower;

    public int Power
    {
        get { return power; }
        set
        {
            power = value;
            powerText.text = "Power: " + power;
        }
    }

    void Start()
    {
        Power = startingPower;
    }
}