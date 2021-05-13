using System.Collections.Generic;
using System;

namespace ArcaneRecursion
{
    public static class ClassLibrary
    {
        public static Dictionary<ClassNames, SkillData[]> ClassDefs = new Dictionary<ClassNames, SkillData[]>()
        {
            { // ArcaneKnight
                ClassNames.ArcaneKnight,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Augmenter
                ClassNames.Augmenter,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // BladeDancer
                ClassNames.BladeDancer,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Champion
                ClassNames.Champion,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(Interception)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Dominator
                ClassNames.Dominator,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Druid
                ClassNames.Druid,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Elementalist
                ClassNames.Elementalist,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Preserver
                ClassNames.Preserver,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // PrimalKnight
                ClassNames.PrimalKnight,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Sentinel
                ClassNames.Sentinel,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Shaman
                ClassNames.Shaman,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Sorcerer
                ClassNames.Sorcerer,
                new SkillData[6]
                {
                    new SkillData(typeof(ArcaneFlow)),
                    new SkillData(typeof(ArcaneBolt)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // VoidKnight
                ClassNames.VoidKnight,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Warden
                ClassNames.Warden,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Warrior
                ClassNames.Warrior,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },
        };
    }

    [Serializable]
    public class SkillData
    {
        public string Name { get; }
        public Type Skill;
        public SkillDefinition SkillDefinition;

        public SkillData(Type skill)
        {
            Name = skill.ToString().Replace("ArcaneRecursion.", "");
            Skill = skill;
            SkillDefinition = null;
        }
    }
}
