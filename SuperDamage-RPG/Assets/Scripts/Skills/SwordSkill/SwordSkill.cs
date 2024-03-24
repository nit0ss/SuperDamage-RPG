using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;


public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}


public class SwordSkill : Skills
{

    public SwordType swordType = SwordType.Regular;

    [Header("Bounce Info")]
    [SerializeField] private int ammountOfBounces;
    [SerializeField] private int bounceGravity;


    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchFornce;
    [SerializeField] private float swordGravity; 

    [Header("Aim dots")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private Transform dotsParten;
    private GameObject[] dots;

    private Vector2 finalDir;



    protected override void Start()
    {
        base.Start();
        GenerateDots();
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
            finalDir = new Vector2(AimDirection().normalized.x * launchFornce.x, AimDirection().normalized.y * launchFornce.y);

        if (Input.GetKey(KeyCode.E))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                //DotsPosition(t) in this case t is dotNumber*spaceBetween, for loop increments time and the more it ++ more time it is so further is the dot.
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }




    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        SwordSkillController newSwordScript = newSword.GetComponent<SwordSkillController>();

        //implementar switch mas adelante
        if (swordType == SwordType.Bounce)
        {
            swordGravity = bounceGravity;
            newSwordScript.SetupBounce(true,bounceGravity,4);
        }


        newSwordScript.SetupSword(finalDir, swordGravity, player);

        player.AssignNewSword(newSword);

        DotsActive(false);
    }




    //******************AIM******************
    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {

        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }


    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParten);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {

        // Calcula la posición parabólica: posición = pos_inicial + vel_inicial*t + 0.5*gravedad*t^2
        Vector2 position = (Vector2)player.transform.position +
        AimDirection().normalized * launchFornce * t
        + 0.5f * Physics2D.gravity * swordGravity * (t * t);


        /* Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchFornce.x,
            AimDirection().normalized.y * launchFornce.y) * t
            * .5f * (Physics2D.gravity * swordGravity * (t * t)); */
        return position;

    }

}
