using UnityEngine;
//Pico perforador se lanza sobre un enemigo y lo atraviesa con el pico causando mucho daño
public class PicoPerforador : Ability
{
    public float speedBuff;
    public int damage;
    public float range;
    CharacterController controller;
    public override void Use()
    {

       
    }
    private void Awake()
    {
        player = GetComponentInParent<PlayerScript>();
        controller = GetComponentInParent<CharacterController>();
        Abilityname = "Pico Perforador";
        damage = 50;
        description = "Se lanza sobre un enemigo y lo atraviesa con el pico causando "+ damage + " daño";
        cooldown = 10f;
        timer = 0f;
        isUsable = true;
        cost = 10;
        speedBuff =30;
        range = 5f;

    }
}
