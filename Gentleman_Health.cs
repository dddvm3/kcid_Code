using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gentleman_Health : MonoBehaviour
{
    public static Gentleman_Health Instance;
    private Gentleman_Master gentleman_Master;
    private Gentleman_Ani ani;
    private int gentlemanhp;
    [Header("Boss血量")]
    public int InitHp = 500;
    public int Gentleman_Hp
    {
        get{return gentlemanhp;}
        set{
            gentlemanhp = value;
            // int hpCount;
            // if(value%100==0)
            // {
            //     hpCount = value/100-1;
            // }
            // else
            // {
            //     hpCount = value/100;
            // }
            // for(int i = 0;i<HP_Count.Length;i++)
            // {
            //     if(i<hpCount)
            //     {
            //         HP_Count[i].gameObject.SetActive(true);
            //     }
            //     else
            //     {
            //         HP_Count[i].gameObject.SetActive(false);
            //     }
            // }
            int fillamount = value;
            // if(gentlemanhp==InitHp)
            // {
            //     fillamount = 100;
            // }
            // else
            // {
            //     fillamount = gentlemanhp%100;
            //     if(fillamount==0) fillamount = 100;
            // }
            NowHpText.text = fillamount.ToString();
            float amount = (float)value/(float)InitHp;
            // if(gentlemanhp==InitHp)
            // {
            //     amount = 1;
            // }
            // else
            // {
            //     amount = ((float)gentlemanhp/(float)100)%1;
            //     if(amount==0) amount = 1;
            // }
            Now_HpFillcCount.fillAmount = amount;
            if(value<=0)
            {
                Gentleman_SummonandSwingstick.Instance.DesAll();
                InteractList.Instance.InteractGoList.Clear();
                GameSystem.Instance.FadeOutBoss();
                GameSystem.Instance.ClearGameEvent();
            }
        }
    }
    [Header("血量UI")]
    public Image Gentleman_Hp_Image;
    
    [Header("被打多少血瞬移")]
    public int Buckle;
    public Image[] HP_Count;
    public Image Now_HpFillcCount;
    public Text NowHpText;
    private int SavededutBlood;
    void Awake()
    {
        Instance = this;
        Gentleman_Hp = InitHp;
    }
    void OnEnable() 
    {
        SetInitialReferences();
        gentleman_Master.EventEnemyDeductHealth += DedutHealth;
    }
    void OnDisable() 
    {
        gentleman_Master.EventEnemyDeductHealth -= DedutHealth;
    }
    void Update()
    {
        if(SavededutBlood >= Buckle)
        {
            gentleman_Master.Can_Teleport = true;
            ani.PlayTeleport();
            AfewBloodTeleport();
        }
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
        ani = GetComponent<Gentleman_Ani>();
    } 
    public void DedutHealth(int HealthChange)
    {
        Gentleman_Hp -= HealthChange;
        SavededutBlood += HealthChange;
    }
    void AfewBloodTeleport()
    {
        SavededutBlood = 0;
    }
    void CanUseTeleportCd()
    {
        gentleman_Master.Can_Teleport = false;
    }
}