using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ArcaneRecursion
{
    public static class ClassLibrary
    {
        public static Dictionary<ClassNames, SkillDefinition[]> ClassDefs = new Dictionary<ClassNames, SkillDefinition[]>()
        {
            { // ArcaneKnight
                ClassNames.ArcaneKnight,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Augmenter
                ClassNames.Augmenter,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // BladeDancer
                ClassNames.BladeDancer,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Champion
                ClassNames.Champion,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(Interception)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Dominator
                ClassNames.Dominator,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Druid
                ClassNames.Druid,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Elementalist
                ClassNames.Elementalist,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Preserver
                ClassNames.Preserver,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // PrimalKnight
                ClassNames.PrimalKnight,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Sentinel
                ClassNames.Sentinel,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Shaman
                ClassNames.Shaman,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Sorcerer
                ClassNames.Sorcerer,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(ArcaneFlow)),
                    new SkillDefinition(typeof(ArcaneBolt)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // VoidKnight
                ClassNames.VoidKnight,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Warden
                ClassNames.Warden,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },

            { // Warrior
                ClassNames.Warrior,
                new SkillDefinition[6]
                {
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                    new SkillDefinition(typeof(NotImplementedCombatSkill)),
                }
            },
        };
    }

    [Serializable]
    public class SkillDefinition
    {
        public Type Skill;
        public NodeDefinition NodeDefinition;
        public SelectionCursor Cursor;//TODO LINK ME

        public SkillDefinition(Type skill) { Skill = skill; NodeDefinition = null; Cursor = null; }
    }
}
