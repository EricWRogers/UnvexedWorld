using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCraft : MonoBehaviour
{
    public enum CastType
    {
        melee,
        ranged
    }
    public enum Aspect
    {
        none,
        scavenge,
        splendor,
        sunder,
    }
    public SpellShot spellShot;
    public Aspect mainAspect = Aspect.none;
    public Aspect modAspect = Aspect.none;
    public float scavengeMana = 100f;
    public float splendorMana = 100f;
    public bool casting = false;
    public bool direction = false;

    PlayerGamepad gamepad;
 
     
    public Spell[] spells;


     void Awake()
    // Start is called before the first frame update
    {
        spells = GetComponentsInChildren<Spell>();
        
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Scavange.performed += ctx => Scavenge();
        gamepad.GamePlay.Splendor.performed += ctx => Splendor();
        gamepad.GamePlay.Clear.performed += ctx => ClearSpell();
        gamepad.GamePlay.Casting.performed += ctx => SetCasting();
         gamepad.GamePlay.Casting.canceled += ctx => UnsetCast();

    }

    void OnEnable()
    {
        gamepad.GamePlay.Enable();
    }

    void OnDisable()
    {
        gamepad.GamePlay.Disable();
    }
    void Scavenge()
    {
        if ( mainAspect == Aspect.none)
        {
            mainAspect = Aspect.scavenge;
        }
        else if ( mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.scavenge;
        }
    }
    void Splendor()
    {
        if ( mainAspect == Aspect.none)
        {
            mainAspect = Aspect.splendor;
        }
        else if ( mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.splendor;
        }
    }

    void ClearSpell()
    {
        mainAspect = Aspect.none;
        modAspect = Aspect.none;
    }

    void SetCasting()
    {
        casting = true;
    }
  void UnsetCast()
    {
        casting = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            SetCasting();
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            UnsetCast();
        }
        GetComponent<Animator>().SetBool("Casting", casting);
        if (Input.GetKeyDown(KeyCode.Q) && casting && mainAspect != Aspect.none)
        {
            //CastSpell(CastType.melee,mainAspect,modAspect);
        }
        else if (Input.GetKeyDown(KeyCode.E) /*&& casting && mainAspect != Aspect.none*/)
        {
            direction = true;
            //CastSpell(CastType.ranged,mainAspect,modAspect);
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            direction = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //mainAspect = Aspect.scavenge;
            Scavenge();
        }

        GetComponent<Animator>().SetBool("CheckDirection", direction);
       
        // if (Input.GetKeyDown(KeyCode.Alpha3) && mainAspect == Aspect.none)
        // {
        //     mainAspect = Aspect.sunder;
        // }
        // else if (Input.GetKeyDown(KeyCode.Alpha3) && mainAspect != Aspect.none && modAspect == Aspect.none)
        // {
        //     modAspect = Aspect.sunder;
        // }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Splendor();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ClearSpell();
        }

        RegenMana(20*Time.deltaTime);

        
    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

   

    public void CastSpell(CastType castType)
    {
        Debug.Log("Casting a " + mainAspect.ToString() + " spell with " + modAspect.ToString() + " modifications at " + castType.ToString() + " range.");
        if (castType == CastType.ranged)
        {
            spellShot.ShootSpellPrefab(mainAspect, modAspect);
           
        }
        if (castType == CastType.melee)
        {
            for(int i = 0; i<spells.Length; i++)
            {
                spells[i].mainAspect = mainAspect;
                spells[i].modAspect = modAspect;
            }
            //GetComponentsInChildren<Spell>().modAspect = modAspect;
            if (mainAspect == Aspect.scavenge)
            {
                GetComponentInChildren<Spell>().lifeSteal = true;
            }
            else
            {
                GetComponentInChildren<Spell>().lifeSteal = false;
            }
            
        }
    }
    public void SpendMana()
    {
        if (mainAspect == Aspect.splendor && splendorMana>=50)
        {
            if (modAspect == Aspect.splendor && splendorMana>=70)
            {
                splendorMana -= 70;
            }
            else if(modAspect == Aspect.scavenge && scavengeMana>=20)
            {
                scavengeMana -= 20;
                splendorMana-=50;
            }
            else if (modAspect == Aspect.none)
            {
                
                splendorMana-=50;
            }
            else
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        else if (mainAspect == Aspect.scavenge && scavengeMana>=50)
        {
            if (modAspect == Aspect.splendor && splendorMana>=20)
            {
                splendorMana -= 20;
                scavengeMana-=50;
            }
            else if(modAspect == Aspect.scavenge && scavengeMana>=70)
            {
                scavengeMana -= 70;
            }
            else if (modAspect == Aspect.none)
            {
                
                scavengeMana-=50;
            }
            else
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        else if (mainAspect == Aspect.none)
        {
            if (modAspect == Aspect.splendor && splendorMana>=20)
            {
                splendorMana -= 20;
            }
            else if(modAspect == Aspect.scavenge && scavengeMana>=20)
            {
                scavengeMana -= 20;
            }
            else if (modAspect == Aspect.none)
            {

            }
            else
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        else
        {
            GetComponent<Animator>().Play("Idle");
        }
    }

    public void RegenMana(float amount)
    {
        splendorMana += amount;
        scavengeMana += amount;
        if(splendorMana>100)
        {
            splendorMana=100;
        }
        
        if(scavengeMana>100)
        {
            scavengeMana=100;
        }
    }
}
