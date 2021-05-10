using System.Collections.Generic;
using System;

namespace ArcaneRecursion
{
    public class LiveSkill
    {
        public CombatSkillObject CombatSkillObject;
        public int Cooldown;

        public LiveSkill(CombatSkillObject combatSkillObject)
        {
            CombatSkillObject = combatSkillObject;
            Cooldown = 0;
        }
    }

    public class UnitSkills
    {
        public List<LiveSkill>[] LiveSkills { get; private set; }
        public LiveSkill SelectedSkill { get { return (_buildCategory != -1 && _spellIndex != -1) ? LiveSkills[_buildCategory][_spellIndex] : null; } }

        private int _buildCategory;
        private int _spellIndex;

        public UnitSkills(List<ClassBuild> build, UnitController unit)
        {
            LiveSkills = new List<LiveSkill>[4] { null, null, null, null };

            for (int classIndex = 0; classIndex < build.Count; classIndex++)
            {
                LiveSkills[classIndex] = new List<LiveSkill>();

                for (int i = 0; i < build[classIndex].Skills.Count; i++)
                {
                    LiveSkills[classIndex].Add(new LiveSkill(build[classIndex].Skills[i]));
                    if (build[classIndex].Skills[i].IsPassive)
                    {
                        (Activator.CreateInstance(Type.GetType("ArcaneRecursion." + build[classIndex].Skills[i].Skill.ToString())) as CombatSkill)
                        .OnSkillLaunched(unit, build[classIndex].Skills[i], null, null);
                    }
                }
            }
            ClearSelectedSkill();
        }

        public void OnStartTurn()
        {
            for (int classIndex = 0; classIndex < 4; classIndex++)
            {
                if (LiveSkills[classIndex] != null)
                {
                    for (int i = 0; i < LiveSkills[classIndex].Count; i++)
                    {
                        if (LiveSkills[classIndex][i].Cooldown > 0)
                            LiveSkills[classIndex][i].Cooldown--;
                    }
                }
            }
        }

        public bool SelectCombatSkill(int buildCategory, int spellIndex)//TODO UI
        {
            if (LiveSkills[buildCategory][spellIndex].Cooldown <= 0)
            {
                _buildCategory = buildCategory;
                _spellIndex = spellIndex;
                return true;
            }
            return false;
        }

        public void OnSkillLaunched()
        {
            LiveSkills[_buildCategory][_spellIndex].Cooldown = LiveSkills[_buildCategory][_spellIndex].CombatSkillObject.Cooldown;
        }

        public void ClearSelectedSkill()
        {
            _buildCategory = -1;
            _spellIndex = -1;
        }
    }
}