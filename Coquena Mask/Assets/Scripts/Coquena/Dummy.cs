using UnityEngine;

public class Dummy : Enemy
{
    public override void Aturdir(float duration)
    {
        Debug.Log("fui brutalmente aturdido por un gil");
    }

    void Start()
    {
        maxHp = 100f;
        currentHp = maxHp;
        speed = 25f;
    }
    

}
