using UnityEngine;
using SuperPupSystems.StateMachine;

public class CrystalPrison : MonoBehaviour
{
    [SerializeField]
    private GruntStateMachine grunt;
    [SerializeField]
    private AgroGruntStateMachine agro;
    [SerializeField]
    private RangeGruntStateMachine range;
    [SerializeField]
    private BossStateMachine boss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grunt = GetComponentInParent<GruntStateMachine>();
        agro = GetComponentInParent<AgroGruntStateMachine>();
        range = GetComponentInParent<RangeGruntStateMachine>();
        boss = GetComponentInParent<BossStateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        if(grunt != null)
        {
            grunt.ChangeState(nameof(CrystallizedState));
        }
        else if(agro != null)
        {
            agro.ChangeState(nameof(CrystallizedState));
        }
        else if(range != null)
        {
            range.ChangeState(nameof(CrystallizedState));
        }
        else if(boss != null)
        {
            boss.ChangeState(nameof(CrystallizedState));
        }
    }
}
