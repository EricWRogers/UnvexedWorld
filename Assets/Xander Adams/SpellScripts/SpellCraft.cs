using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCraft : MonoBehaviour
{

    public enum CastType
    {
        ranged,
        melee,
        self
    }

    public enum Aspect
    {
        none,
        loft,
        weight,
        launch
    }

    public CastType castType = CastType.ranged;
    public Aspect mainAspect = Aspect.none;
    public Aspect modAspect = Aspect.none;
    public bool casting = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PrintCastType(castType);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            castType = ChangeCastLeft(castType);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            castType = ChangeCastRight(castType);
        }
        if (Input.GetKeyDown(KeyCode.V) && !casting)
        {
            casting = true;
        }
        else if (Input.GetKeyDown(KeyCode.V) && casting && mainAspect != Aspect.none)
        {
            CastSpell(castType,mainAspect,modAspect);
            casting = false;
            mainAspect = Aspect.none;
            modAspect = Aspect.none;
        }
        if (Input.GetKeyDown(KeyCode.H) && casting && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.loft;
        }
        else if (Input.GetKeyDown(KeyCode.H) && casting && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.loft;
        }
        if (Input.GetKeyDown(KeyCode.U) && casting && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.launch;
        }
        else if (Input.GetKeyDown(KeyCode.U) && casting && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.launch;
        }
        if (Input.GetKeyDown(KeyCode.K) && casting && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.weight;
        }
        else if (Input.GetKeyDown(KeyCode.K) && casting && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.weight;
        }

    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

    public CastType ChangeCastLeft(CastType castType)
    {
        if (castType == CastType.ranged)
        castType = CastType.melee;
        else if(castType == CastType.melee)
        castType = CastType.self;
        else 
        castType = CastType.ranged;
        return castType;
    }

    public CastType ChangeCastRight(CastType castType)
    {
        if (castType == CastType.ranged)
        castType = CastType.self;
        else if(castType == CastType.melee)
        castType = CastType.ranged;
        else 
        castType = CastType.melee;
        return castType;
    }

    public void CastSpell(CastType castType, Aspect mainAspect, Aspect modAspect)
    {
        Debug.Log("Casting a " + mainAspect.ToString() + " spell with " + modAspect.ToString() + " modifications at " + castType.ToString() + " range.");
    }
}
