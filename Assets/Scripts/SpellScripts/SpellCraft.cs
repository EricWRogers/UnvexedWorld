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
    public Aspect CurrentElement = Aspect.none;
    public Aspect modAspect = Aspect.none;
    public float scavengeMana = 100f;
    public float splendorMana = 100f;
    public int subAspect = 0;
    public bool casting = false;

    public bool mainSet = false;

    public bool modSet = false;

    public bool clear = false;

   
   

    PlayerGamepad gamepad;
 
     
    public Spell[] spells;


     void Awake()
    // Start is called before the first frame update
    {
        spells = GetComponentsInChildren<Spell>();
        
        gamepad = new PlayerGamepad();
        //gamepad.GamePlay.Scavange.performed += ctx => Scavenge();
        //gamepad.GamePlay.Sunder.performed += ctx => Sunder();
        //gamepad.GamePlay.Splendor.performed += ctx => Splendor();
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
        if(Input.GetKeyDown(KeyCode.E))
        {
            SetCasting();
        }
        else if(Input.GetKeyUp(KeyCode.E))
        {
            UnsetCast();
        }
        GetComponent<Animator>().SetBool("Casting", casting);
        
        
        
        //Setting spell components
        //if (Input.GetKeyDown(KeyCode.Alpha1))
       // {
            //CurrentElement = Aspect.scavenge;
            //Scavenge();
        //}

        // if (Input.GetKeyDown(KeyCode.Alpha3))
        // {
        //     Sunder();
        // }

       // if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
           // Splendor();
        //}
        
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //{
           // ClearSpell();
        //}
        
    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

   

    public void CastSpell(CastType castType)
    {
        Debug.Log("Casting a " + CurrentElement.ToString() + " spell with " + modAspect.ToString() + " modifications at " + castType.ToString() + " range.");
        if (castType == CastType.ranged)
        {
            spellShot.ShootSpellPrefab(CurrentElement, modAspect);
           
        }
        if (castType == CastType.melee)
        {
            for(int i = 0; i<spells.Length; i++)
            {
                spells[i].SetSelf(CurrentElement,modAspect);
            }
            
        }
    }
    

    
    //Populating the spell list
    void Scavenge()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.scavenge;
            
        }
        else if ( CurrentElement != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.scavenge;
        }
    }

    void Sunder()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.sunder;
        }
        else if ( CurrentElement != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.sunder;
        }
    }

    void Splendor()
    {
        if ( CurrentElement == Aspect.none)
        {
            CurrentElement = Aspect.splendor;
        }
        else if ( CurrentElement != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.splendor;
        }
    }
    public void SetMain(Aspect aspect)
    {
        CurrentElement = aspect;
        mainSet = true;
    }

    public void SetMod(Aspect aspect)
    {
        modAspect = aspect;
        modSet = true;
    }

    void ClearSpell()
    {
        clear = true;
        CurrentElement = Aspect.none;
        modAspect = Aspect.none;
    }

    //Modifying the spell script on the fist
    public void SetFistMain(Aspect aspect)
    {
        spells[0].SetMain(aspect);
    }

    public void SetFistMod(Aspect aspect)
    {
        spells[0].SetMod(aspect);
    }

    void ClearFistSpell()
    {
        spells[0].ClearSpell();
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
