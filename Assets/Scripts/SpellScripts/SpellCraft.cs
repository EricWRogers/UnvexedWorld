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
   

    PlayerGamepad gamepad;
 
     
    public Spell[] spells;


     void Awake()
    // Start is called before the first frame update
    {
        spells = GetComponentsInChildren<Spell>();
        
        gamepad = new PlayerGamepad();
        gamepad.GamePlay.Scavange.performed += ctx => Scavenge();
        //gamepad.GamePlay.Sunder.performed += ctx => Sunder();
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
        
        
        
        //Setting spell components
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //mainAspect = Aspect.scavenge;
            Scavenge();
        }

        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     Sunder();
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
    

    
    //Populating the spell list
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

    void Sunder()
    {
        if ( mainAspect == Aspect.none)
        {
            mainAspect = Aspect.sunder;
        }
        else if ( mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.sunder;
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
    public void SetMain(Aspect aspect)
    {
        mainAspect = aspect;
    }

    public void SetMod(Aspect aspect)
    {
        modAspect = aspect;
    }

    void ClearSpell()
    {
        mainAspect = Aspect.none;
        modAspect = Aspect.none;
    }

    
    //controlling the casting variable
    void SetCasting()
    {
        casting = true;
    }
  void UnsetCast()
    {
        casting = false;
    }


    //setting listeners for populating list on hit
    public void AddTheListenerMain(SpellCraft.Aspect aspect)
    {
        gameObject.GetComponentsInChildren<PunchScript>()[1].punchTarget.AddListener(delegate{SetMain(aspect);});
    }

    public void AddTheListenerMod(SpellCraft.Aspect aspect)
    {
        gameObject.GetComponentsInChildren<PunchScript>()[1].punchTarget.AddListener(delegate{SetMod(aspect);});
    }
    
    public void RemoveTheListener()
    {
        gameObject.GetComponentsInChildren<PunchScript>()[1].punchTarget.RemoveAllListeners();
    }

    public void AOEOnMe()
    {
        Instantiate(ParticleManager.Instance.AOE, gameObject.transform.position, transform.rotation).AddComponent<HitListener>();
    }
}
