using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gentleman_Ani : MonoBehaviour
{
    private Animator GentlemanAnimator;
    void OnEnable() 
    {
        SetInitialReferences();
    }
    void SetInitialReferences()
    {
        GentlemanAnimator = GetComponent<Animator>();
    }
    public void PlaySummon()
    {
        GentlemanAnimator.SetBool("Summon", true);
    }
    public void CloseSummon()
    {
        GentlemanAnimator.SetBool("Summon", false);
    }
    public void PlaySwingStick()
    {
        GentlemanAnimator.SetTrigger("CrowBar");
    }
    public void PlayTeleport()
    {
        GentlemanAnimator.SetTrigger("Teleport");
    }
    public void PlayHitted()
    {
        GentlemanAnimator.SetTrigger("Hitted");
    }
}
