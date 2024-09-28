using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeRangedAttack : MonoBehaviour
{
    public SpellCraft spellCraft;
    public SpellShot spellShot;
    PlayerGamepad gamepad;

    void Awake()
    {
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Melee.performed += ctx => MeleeGamepad();
         gamepad.GamePlay.Shoot.performed += ctx => Range();
         
    }
    
    // Start is called before the first frame update
     void MeleeGamepad()
        {
            
            if (spellCraft.casting)
            {
                spellCraft.CastSpell(SpellCraft.CastType.melee, spellCraft.mainAspect, spellCraft.modAspect);
            }
            Melee();
        }
    void Start()
    {
        
    }
    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.Q))
        {
           MeleeGamepad();
        }
        else if (Input.GetMouseButtonDown(1)||Input.GetKeyDown(KeyCode.E))
        {
            // if (spellCraft.casting && spellCraft.mainAspect!=SpellCraft.Aspect.none)
            // {
            //     spellCraft.CastSpell(SpellCraft.CastType.ranged, spellCraft.mainAspect, spellCraft.modAspect);
            // }
            
            Range();
        }

        
    }

    private void Melee()
    {
        GetComponent<Animator>().SetTrigger("Melee");
        //GetComponent<AnimationForce>().melee = true;
    }

    private void Range()
    {
        GetComponent<Animator>().SetTrigger("Ranged");
        //GetComponent<AnimationForce>().ranged = true;
        //spellShot.ShootPrefab();
    }

    private void Damage()
    {

    }

    public void StartParticle()
    {
        GetComponentInChildren<PunchScript>().StartParticle();
    }

    public void EndParticle()
    {
        GetComponentInChildren<PunchScript>().EndParticle();
    }

    public void StartParticleSpell()
    {
        GetComponentInChildren<Spell>().gameObject.GetComponent<PunchScript>().StartParticle();
    }

    public void EndParticleSpell()
    {
        GetComponentInChildren<Spell>().gameObject.GetComponent<PunchScript>().EndParticle();
    }
}
