using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_SaluteShoot : MonoBehaviour
{
    public AudioSource ShootAudio;
    private Gentleman_Master gentleman_Master;
    [Header("自定義的子彈")]
    public GameObject Boss_Bullet;
    [Header("複製產生點")]
    public Transform[] Shoot_Pos;
    [Header("射出次數")]
    public int ShootTime;
    void OnEnable() 
    {
        SetInitialReferences();
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SaluteShoot();
        }
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
    }
    void SaluteShoot()
    {
        ShootAudio.Play();
        gentleman_Master.SaluteShoot = true;
        for(int t = 0; t < ShootTime; t++)
        {
            for (int i = 0; i < Shoot_Pos.Length; i++)
            {
                GameObject Bullet = Instantiate(Boss_Bullet,Shoot_Pos[i].position, Shoot_Pos[i].rotation);
                Bullet.transform.LookAt(ThirdPersonController.m.transform.position);
                Bullet.name = "EnemyBullet";
                Bullet.SetActive(true);
            }
        }
    }
    void HasSaluteShoot()
    {
        gentleman_Master.SaluteShoot = false;
    }
}