using System.Collections.Generic;
using System;

namespace ArcaneRecursion
{
    public static class ClassSkillLibrary
    {
        public static Dictionary<ClassNames, SkillData[]> ClassSkillsDatas = new Dictionary<ClassNames, SkillData[]>()
        {
            #region ArcaneKnight
            {
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
            #endregion /* ArcaneKnight */

            #region Augmenter
            {
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
            #endregion /* Augmenter */

            #region BladeDancer
            {
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
            #endregion /* BladeDancer */

            #region Champion
            {
                ClassNames.Champion,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(Interception)),
                    new SkillData(typeof(Whirlwind)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },
            #endregion /* Champion */

            #region Dominator
            {
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
            #endregion /* Dominator */

            #region Druid
            {
                ClassNames.Druid,
                new SkillData[6]
                {
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(Overgrowth)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },
            #endregion /* Druid */

            #region Elementalist
            {
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
            #endregion /* Elementalist */

            #region Preserver
            {
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
            #endregion /* Preserver */

            #region PrimalKnight
            {
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
            #endregion /* PrimalKnight */

            #region Sentinel
            {
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
            #endregion /* Sentinel */

            #region Shaman
            {
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
            #endregion /* Shaman */

            #region Sorcerer
            {
                ClassNames.Sorcerer,
                new SkillData[6]
                {
                    new SkillData(typeof(ArcaneFlow)),
                    new SkillData(typeof(ArcaneBolt)),
                    new SkillData(typeof(ArcaneRush)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                }
            },
            #endregion /* Sorcerer */

            #region VoidKnight
            {
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
            #endregion /* VoidKnight */

            #region Warden
            {
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
            #endregion /* Warden */

            #region Warrior
            {
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
            #endregion /* Warrior */
        };

        public static Dictionary<string, SkillEffectData> ClassEffectsDatas = new Dictionary<string, SkillEffectData>()
        {
            #region Druid
            {
                GetNameFromType(typeof(OvergrowthEffect)),
                new SkillEffectData(typeof(OvergrowthEffect), ClassNames.Druid)
            },
            #endregion /* Druid */

            #region Sorcerer
            {
                GetNameFromType(typeof(ArcaneFlowEffectPassiv)),
                new SkillEffectData(typeof(ArcaneFlowEffectPassiv), ClassNames.Sorcerer)
            },
            {
                GetNameFromType(typeof(ArcaneFlowEffect)),
                new SkillEffectData(typeof(ArcaneFlowEffect), ClassNames.Sorcerer)
            },
            #endregion /* Sorcerer */
        };

        public static string GetNameFromType(Type type) { return type.ToString().Replace("ArcaneRecursion.", ""); }
    }

    [Serializable]
    public class SkillData
    {
        public string Name { get; }
        public Type Skill;
        public SkillDefinition SkillDefinition;

        public SkillData(Type skill)
        {
            Name = ClassSkillLibrary.GetNameFromType(skill);
            Skill = skill;
            SkillDefinition = null;
        }
    }

    [Serializable]
    public class SkillEffectData
    {
        public string Name { get; }
        public ClassNames Class;
        public Type Skill;
        public NodeDefinition EffectDefinition;

        public SkillEffectData(Type skill, ClassNames className)
        {
            Name = ClassSkillLibrary.GetNameFromType(skill);
            Class = className;
            Skill = skill;
            EffectDefinition = null;
        }
    }
}
