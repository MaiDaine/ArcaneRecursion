using UnityEngine;
using System.Collections.Generic;
using System;

namespace ArcaneRecursion
{
    public class UnitSkill
    {
        public SkillData SkillData;
        public SkillStats SkillStats { get { return SkillData.SkillDefinition.SkillStats; } }
        public int Cooldown;

        public UnitSkill(SkillData skillData)
        {
            SkillData = skillData;
            Cooldown = 0;
        }
    }

    public class UnitSkills
    {
        public ClassNames[] BuildClassNames { get; private set; }
        public List<UnitSkill>[] AvailableSkills { get; private set; }
        public UnitSkill SelectedSkill { get; private set; }

        public UnitSkills(List<ClassBuild> build, UnitController unit)
        {
            //TODO Innate skills
            AvailableSkills = new List<UnitSkill>[4] { new List<UnitSkill>(), null, null, null };
            BuildClassNames = new ClassNames[4] { ClassNames.Innate, ClassNames.Innate, ClassNames.Innate, ClassNames.Innate };
            for (int classIndex = 1; classIndex < build.Count; classIndex++)
            {
                BuildClassNames[classIndex] = build[classIndex].Name;
                AvailableSkills[classIndex] = new List<UnitSkill>();
                if (build[classIndex].AvailableSkills[0])
                {
                    SkillData skill = ClassSkillLibrary.ClassSkillsDatas[build[classIndex].Name][0];
                    ((CombatSkill)Activator.CreateInstance(skill.Skill)).OnSkillLaunched(skill.SkillDefinition, unit, null, null);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (build[classIndex].AvailableSkills[i])
                        AvailableSkills[classIndex].Add(new UnitSkill(ClassSkillLibrary.ClassSkillsDatas[build[classIndex].Name][i]));
                }
            }
            ClearSelectedSkill();
        }

        public void OnStartTurn()
        {
            for (int classIndex = 0; classIndex < 4; classIndex++)
            {
                if (AvailableSkills[classIndex] != null)
                {
                    for (int i = 0; i < AvailableSkills[classIndex].Count; i++)
                        if (AvailableSkills[classIndex][i].Cooldown > 0)
                            AvailableSkills[classIndex][i].Cooldown--;
                }
                else
                    return;
            }
        }

        public bool SelectCombatSkill(int buildCategory, int spellIndex)//TODO UI
        {
            if (AvailableSkills[buildCategory][spellIndex].Cooldown <= 0)
            {
                SelectedSkill = AvailableSkills[buildCategory][spellIndex];
                return true;
            }
            return false;
        }

        public void OnSkillLaunched()
        {
            SelectedSkill.Cooldown = SelectedSkill.SkillStats.Cooldown;
        }

        public void ClearSelectedSkill()
        {
            SelectedSkill = null;
        }
    }
}