using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ArcaneRecursion
{
    public class UnitSkillsStub : UnitSkills
    {
        public UnitSkillsStub(UnitSkills other)
        {
            AvailableSkills = new List<UnitSkill>[4];
            for (int i = 0; i < 4; i++)
            {
                if (other.AvailableSkills[i] != null)
                    AvailableSkills[i] = other.AvailableSkills[i].ConvertAll(e =>
                    {
                        UnitSkill skill = new UnitSkill(e.SkillData);
                        skill.Cooldown = e.Cooldown;
                        return skill;
                    });
                else
                    break;
            }
        }
    }
}