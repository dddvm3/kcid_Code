using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_SummonandSwingstick : MonoBehaviour
{
    public static Gentleman_SummonandSwingstick Instance;
    public List<GameObject> BombList;
    private Gentleman_Master gentleman_Master;
    [Header("炸彈怪物件")]
    public GameObject BombMonster;
    [Header("召喚炸彈怪特效")]
    public GameObject ProduceBombParticle;
    [Header("中心點")]
    public Transform SummonCenterPoint;
    [Header("圓圈範圍")]
    public float CircleRange;
    [Header("炸彈怪生成數量")]
    public int Bombcount;
    [Header("炸彈怪生成數量上限")]
    public int BombcountLimit = 3;
    public float OffsetY;
    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        SetInitialReferences();
    }
    void SetInitialReferences()
    {
        gentleman_Master = GetComponent<Gentleman_Master>();
    }
    void Update() 
    {
        // if (CanCheckBombCount)
        // {
        //     if (NowInfield.Count >= Bombcount || NowInfield.Count > 0)
        //     {
        //         NowInfieldBomb = NowInfield.Count;
        //         for (int i = 0; i < NowInfield.Count - 1; i++)
        //         {
        //             if (NowInfield[i].gameObject == null)
        //             {
        //                 NowInfield.Remove(NowInfield[i]);
        //             }
        //         }
        //     }
        // }   
        // if(NowInfield.Count == 0)
        // {
        //     CanCheckBombCount = false;
        // }
        // if(DisPlayerandGentleman())
        // {
        //     if(CheckSwingstickCD())
        //     {
        //         gentleman_Ani.PlaySwingStick();
        //     }
        // }
        // else
        // {
        //     if (!CanCheckBombCount) 
        //     {
        //         gentleman_Ani.PlaySummon();
        //     }
        // }
    }
    public void DesAll()
    {
        for(int i = 0;i<BombList.Count;i++)
        {
            Destroy(BombList[i]);
        }
    }
    void SummonBomb()
    {
        //CanCheckBombCount = true;
        for(int i =0; i < Bombcount ;i++)
        {
            Vector2 p = Random.insideUnitCircle * CircleRange;
            Vector2 pos = p.normalized * (1 + p.magnitude);
            Vector3 BronPos = new Vector3 ( SummonCenterPoint.position.x + pos.x, SummonCenterPoint.position.y, SummonCenterPoint.position.z + pos.y);
            if(BombList.Count<BombcountLimit)
            {
                GameObject Bomb = Instantiate(BombMonster, new Vector3( BronPos.x, BronPos.y - 31, BronPos.z), Quaternion.identity);
                BombList.Add(Bomb);
                Bomb.name = "Bomb";
                GameObject BombParticle = Instantiate( ProduceBombParticle, new Vector3( Bomb.gameObject.transform.position.x, Bomb.gameObject.transform.position.y + OffsetY, Bomb.gameObject.transform.position.z), Quaternion.identity);
                BombParticle.SetActive(true);
                BombParticle.transform.parent = null;
                Bomb.SetActive(true);
            }
        }
    }
    
}