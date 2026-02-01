using UnityEngine;

public class Dummy : Enemy
{
    
    void Start()
    {
        maxHp = 100f;
        currentHp = maxHp;
        speed = 25f;
    }
}
