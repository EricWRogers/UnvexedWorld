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
    public bool casting = false;

     PlayerGamepad gamepad;
 
     void Awake()
    public Spell[] spells;
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
        else if (Input.GetKeyDown(KeyCode.E) && casting && mainAspect != Aspect.none)
        {
            //CastSpell(CastType.ranged,mainAspect,modAspect);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //mainAspect = Aspect.scavenge;
            Scavenge();
        }
       
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

        
    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

   

    public void CastSpell(CastType castType, Aspect mainAspect, Aspect modAspect)
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
}
