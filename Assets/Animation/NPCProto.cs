using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProto : MonoBehaviour
{
    public bool IsWalking ;
    public bool IsTalking ;
    public bool IsSitting;
    public bool IsIdleVendor ;
    public bool IsDrunkingWalk ;
    public bool IsDrunking ;

    Animator NPC;

    private void Awake()
    {
        NPC = GetComponent<Animator>();
    }

    void Start()
    {
        NPC.SetBool("Walk", IsWalking) ;
        NPC.SetBool("Talk", IsTalking) ;
        NPC.SetBool("Sit", IsSitting) ;
        NPC.SetBool("IdleVendor", IsIdleVendor) ;
        NPC.SetBool("DrunkWalk", IsDrunkingWalk) ;
        NPC.SetBool("Drunk", IsDrunking) ;
    }

    void Update()
    {
       
        
    }
}
