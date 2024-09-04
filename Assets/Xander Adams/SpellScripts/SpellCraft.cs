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
        pierce,
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
            mainAspect = Aspect.pierce;
        }
        else if (Input.GetKeyDown(KeyCode.U) && casting && mainAspect != Aspect.none && modAspect == Aspect.none)
        {
            modAspect = Aspect.pierce;
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
                else if(modAspect == Aspect.pierce)
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
            if(mainAspect == Aspect.pierce)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards and up and makes them floaty");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target exceptionally far");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Shoot a piercing projectile that launches the target backwards and downwards and prones them");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Shoot a piercing projectile that launches a target backwards and draws objects towards the target as it flies");
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
                else if(modAspect == Aspect.pierce)
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
                else if(modAspect == Aspect.pierce)
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
                else if(modAspect == Aspect.pierce)
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
            if(mainAspect == Aspect.pierce)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("A rush punch that bounces the user off of the enemy");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("A rush punch uppercut that makes enemies floaty");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("A piercing punch that goes through enemies and then happens again (choose next direction?)");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("A rush haymaker that grounds targets");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("A rush punch that magnetizes the target");
                }
            }
            if(mainAspect == Aspect.weight)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("A haymaker that grounds targets");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("A haymaker that grounds targets then makes them slowly float");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("A haymaker that grounds targets and ???");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("A haymaker that grounds targets and creates a grounding shockwave");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("A haymaker that grounds targets and magnetizes them");
                }
            }
            if(mainAspect == Aspect.magnet)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("create a magnetized field and punch as you get pulled in");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("create a magnetized field and punch as you get pulled in and makes hit targets floaty");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("create a magnetized field and punch as you get pulled in and ???");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("create a magnetized field and punch as you get pulled in grounding hit targets");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("create a magnetized field and punch as you get pulled in strongly magnetizing targets");
                }
            }
        }
        if (castType == CastType.self)
        {
            if(mainAspect == Aspect.loft)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("make self floaty");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("increase jump height and make self floaty");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("launch self upwards???");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Launch self upwards then slam down causing a grounding shockwave");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("make self floaty && ???");
                }
            }
            if(mainAspect == Aspect.pierce)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Dash the direction you are looking");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Dash the direction you are looking and become floaty");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("big dash??? double dash??? Liam input please???");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Dash the direction you are looking then slam down");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Dash the direction you are looking and become magnetized?");
                }
            }
            if(mainAspect == Aspect.weight)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("slam down causing a grounding shockwave");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("slam down causing a grounding shockwave && ??? (Loft)");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("slam down causing a grounding shockwave && ??? (Pierce)");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("bigger shockwave");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("slam down causing a grounding shockwave and magnitize self???");
                }
            }
            if(mainAspect == Aspect.magnet)
            {
                if(modAspect == Aspect.none)
                {
                    Debug.Log("Self magnet is wacky");
                }
                else if(modAspect == Aspect.loft)
                {
                    Debug.Log("Self magnet is wacky and I don't kow how to modify it (Floaty)");
                }
                else if(modAspect == Aspect.pierce)
                {
                    Debug.Log("Self magnet is wacky and I don't kow how to modify it (Pierce)");
                }
                else if(modAspect == Aspect.weight)
                {
                    Debug.Log("Self magnet is wacky and I don't kow how to modify it (Weight)");
                }
                else if(modAspect == Aspect.magnet)
                {
                    Debug.Log("Self magnet is wacky but make it bigger");
                }
            }
        }
    }
}
