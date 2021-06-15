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
        public List<UnitSkill>[] AvailableSkills { get; protected set; }
        public UnitSkill SelectedSkill { get; protected set; }

        protected readonly List<CombatEffect> _trackedEffects;

        //Stub constructor
        public UnitSkills() { _trackedEffects = new List<CombatEffect>(); }

        public UnitSkills(List<SkillData> innateSkills, List<ClassBuild> build, UnitController unit)
        {
            AvailableSkills = new List<UnitSkill>[4] { new List<UnitSkill>(), null, null, null };
            _trackedEffects = new List<CombatEffect>();
            BuildClassNames = new ClassNames[4] { ClassNames.Innate, ClassNames.Innate, ClassNames.Innate, ClassNames.Innate };

            foreach (SkillData e in innateSkills)
                AvailableSkills[0].Add(new UnitSkill(e));

            for (int classIndex = 1; classIndex < build.Count; classIndex++)
            {
                BuildClassNames[classIndex] = build[classIndex].Name;
                AvailableSkills[classIndex] = new List<UnitSkill>();
                if (build[classIndex].AvailableSkills[0])
                {
                    SkillData skill = SkillLibrary.ClassSkillsDatas[build[classIndex].Name][0];
                    ((CombatSkill)Activator.CreateInstance(skill.Skill)).OnSkillLaunched(skill.SkillDefinition, unit, null, null);
                }
                for (int i = 1; i < 6; i++)
                {
                    if (build[classIndex].AvailableSkills[i])
                        AvailableSkills[classIndex].Add(new UnitSkill(SkillLibrary.ClassSkillsDatas[build[classIndex].Name][i]));
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

            _trackedEffects.RemoveAll(e => e.Duration == 0);
        }

        #region TrackedEffect
        public void AddTrackedEffect(CombatEffect effect) { _trackedEffects.Add(effect); }

        public CombatEffect GetTrackedEffect(string name) { return _trackedEffects.Find(e => e.Name == name); }
        #endregion /* TrackedEffect */

        #region SkillLaunchCycle
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
        #endregion /* SkillLaunchCycle */
    }
}