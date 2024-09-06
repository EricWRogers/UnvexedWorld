using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        casting = Input.GetKey(KeyCode.F);
        if (Input.GetKeyDown(KeyCode.Q)&&casting)
        {
            CastSpell(CastType.melee,mainAspect,modAspect);
        }
        else if (Input.GetKeyDown(KeyCode.E)&&casting)
        {
            CastSpell(CastType.ranged,mainAspect,modAspect);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.scavenge;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1) && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.scavenge;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.sunder;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.sunder;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.splendor;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.splendor;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mainAspect = Aspect.none;
            modAspect = Aspect.none;
        }
    }

    public void PrintCastType(CastType castType)
    {
        Debug.Log("" + castType.ToString());
    }

    // public CastType ChangeCastLeft(CastType castType)
    // {
    //     if (castType == CastType.ranged)
    //     castType = CastType.melee;
    //     else if(castType == CastType.melee)
    //     castType = CastType.self;
    //     else 
    //     castType = CastType.ranged;
    //     return castType;
    // }

    // public CastType ChangeCastRight(CastType castType)
    // {
    //     if (castType == CastType.ranged)
    //     castType = CastType.self;
    //     else if(castType == CastType.melee)
    //     castType = CastType.ranged;
    //     else 
    //     castType = CastType.melee;
    //     return castType;
    // }

    public void CastSpell(CastType castType, Aspect mainAspect, Aspect modAspect)
    {
        Debug.Log("Casting a " + mainAspect.ToString() + " spell with " + modAspect.ToString() + " modifications at " + castType.ToString() + " range.");
        if (castType == CastType.ranged)
        {
            if(mainAspect == Aspect.scavenge)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot projectile that launches target into the air and then makes them floaty");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("Shoot a projectile that launches the target into the air and suspends them there longer???");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("Shoot a projectile that launches the target further up and makes them floaty");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("Shoot a projectile that launches a target into the air and then slams them to the ground");
                }
            }
            if(mainAspect == Aspect.sunder)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards and up and makes them floaty");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target exceptionally far");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards and downwards and prones them");
                }
            }
            if(mainAspect == Aspect.splendor)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground and prones them");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground and then causes them to float upwards");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground creating a shockwave that launches nearby objects");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground, knocks them prone, and applies a few seconds of slowness");
                }
            }
        }
        if (castType == CastType.melee)
        {
            if(mainAspect == Aspect.scavenge)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and makes them floaty");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and suspends them there for a longer duration");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and away from each other");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("An uppercut that launches the target and user into the air then slams both into the ground");
                }
            }
            if(mainAspect == Aspect.sunder)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("A rush punch that bounces the user off of the enemy");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("A rush punch uppercut that makes enemies floaty");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("A piercing punch that goes through enemies and then happens again (choose next direction?)");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("A rush haymaker that grounds targets");
                }
            }
            if(mainAspect == Aspect.splendor)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("A haymaker that grounds targets");
                }
                else if(modAspect == Aspect.scavenge)
                {
                    Debug.Log("A haymaker that grounds targets then makes them slowly float");
                }
                else if(modAspect == Aspect.sunder)
                {
                    Debug.Log("A haymaker that grounds targets and ???");
                }
                else if(modAspect == Aspect.splendor)
                {
                    Debug.Log("A haymaker that grounds targets and creates a grounding shockwave");
                }
            }
        }
    }
}
