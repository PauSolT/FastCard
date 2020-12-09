using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour 
{
    public enum EnemyAction
    {
        Attack,
        Defend,
        Heal,
        Status,
        Multiple
    }
    public Dictionary<EnemyAction, System.Action> options = new Dictionary<EnemyAction, System.Action>();

    public void ChooseOption()
    {
        int numRandom = Random.Range(0, options.Count - 1);
        switch(numRandom)
        {
            case 0:
                FirstOption();
                break;
            case 1:
                SecondOption();
                break;
            case 2:
                ThirdOption();
                break;
            case 3:
                FourthOption();
                break;
            case 4:
                FifthOption();
                break;
            default:
                break;
        }
    }

    public virtual void Init()
    {

    }

    public virtual void FirstOption()
    {

    }

    public virtual void SecondOption()
    {

    }

    public virtual void ThirdOption()
    {

    }

    public virtual void FourthOption()
    {

    }

    public virtual void FifthOption()
    {

    }

    public virtual void StartingPassive()
    {

    }

    public virtual void PassiveOption()
    {

    }
}
