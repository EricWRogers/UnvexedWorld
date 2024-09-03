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
        launch,
        magnet
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
        if (Input.GetKeyDown(KeyCode.J) && casting && mainAspect == Aspect.none)
        {
            mainAspect = Aspect.magnet;
        }
        else if (Input.GetKeyDown(KeyCode.J) && casting && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.magnet;
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
        if (castType == CastType.ranged)
        {
            if(mainAspect == Aspect.loft)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot projectile that launches target into the air and then makes them floaty");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Shoot a projectile that launches the target into the air and suspends them there longer???");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("Shoot a projectile that launches the target further up and makes them floaty");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Shoot a projectile that launches a target into the air and then slams them to the ground");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Shoot a projectile that launches the target into the air and then draws objects towards the floating target");
                }
            }
            if(mainAspect == Aspect.launch)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot projectile that launches the target backwards");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Shoot projectile that launches the target backwards and up and makes them floaty");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("Shoot projectile that launches the target exceptionally far");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Shoot projectile that launches the target backwards and downwards and prones them");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Shoot projectile that launches a target backwards and draws objects towards the target as it flies");
                }
            }
            if(mainAspect == Aspect.weight)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground and prones them");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground and then causes them to float upwards");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground creating a shockwave that launches nearby objects");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground, knocks them prone, and applies a few seconds of slowness");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Shoot projectile that slams a target into the ground and draws things towards the prone target");
                }
            }
            if(mainAspect == Aspect.magnet)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot a homing projectile. Target becomes magnetized");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Shoot a homing projectile. Target becomes floaty and magnetized");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("Shoot a homing projectile. Target is knocked back and magnetized");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Shoot a homing projectile. Target is magetized and attracted objects are slammed to the ground");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Shoot a homing projectile. Target becomes heavily magnetized");
                }
            }
        }
        if (castType == CastType.melee)
        {
            if(mainAspect == Aspect.loft)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and makes them floaty");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and suspends them there for a longer duration");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and away from each other");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("An uppercut that launches the target and user into the air then slams both into the ground");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("An uppercut that launches the target and user into the air and makes them floaty. Target is magnetized");
                }
            }
            if(mainAspect == Aspect.launch)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("A rush punch that bounces the user off of the enemy");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("A rush punch uppercut");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
            if(mainAspect == Aspect.weight)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
            if(mainAspect == Aspect.magnet)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
        }
        if (castType == CastType.self)
        {
            if(mainAspect == Aspect.loft)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
            if(mainAspect == Aspect.launch)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
            if(mainAspect == Aspect.weight)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
            if(mainAspect == Aspect.magnet)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.launch)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("");
                }
            }
        }
    }
}
