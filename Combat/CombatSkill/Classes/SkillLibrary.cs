using System.Collections.Generic;
using System;

namespace ArcaneRecursion
{
    public static class SkillLibrary
    {
        public static Dictionary<string, SkillData> InnateSkills = new Dictionary<string, SkillData>()
        {
            {
                GetNameFromType(typeof(BasicAtk)),
                new SkillData(typeof(BasicAtk))
            }
            #region Weapons
            #endregion /* Weapons */
        };

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
                    new SkillData(typeof(ChargedBlade)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(NotImplementedCombatSkill)),
                    new SkillData(typeof(Dive)),
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
                    new SkillData(typeof(UnstoppableForce)),
                    new SkillData(typeof(Interception)),
                    new SkillData(typeof(Whirlwind)),
                    new SkillData(typeof(PowerArmor)),
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
                    new SkillData(typeof(Symbiosis)),
                    new SkillData(typeof(Overgrowth)),
                    new SkillData(typeof(Cleansing)),
                    new SkillData(typeof(LifeLeech)),
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
                    new SkillData(typeof(ManaDownburst)),
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
                    new SkillData(typeof(Gust)),
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
                    new SkillData(typeof(Intervention)),
                    new SkillData(typeof(ArmorOverload)),
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
            #region Augmenter
            {
                GetNameFromType(typeof(ChargedBladeEffectPassiv)),
                new SkillEffectData(typeof(ChargedBladeEffectPassiv), ClassNames.Augmenter)
            },
            {
                GetNameFromType(typeof(ChargedBladeEffect)),
                new SkillEffectData(typeof(ChargedBladeEffect), ClassNames.Augmenter)
            },
            {
                GetNameFromType(typeof(DiveEffect)),
                new SkillEffectData(typeof(DiveEffect), ClassNames.Augmenter)
            },
            #endregion /* Augmenter */

            #region Champion
            {
                GetNameFromType(typeof(UnstoppableForceEffectPassiv)),
                new SkillEffectData(typeof(UnstoppableForceEffectPassiv), ClassNames.Champion)
            },
            {
                GetNameFromType(typeof(PowerArmorEffect)),
                new SkillEffectData(typeof(PowerArmorEffect), ClassNames.Champion)
            },
            #endregion /* Champion */

            #region Druid
            {
                GetNameFromType(typeof(SymbiosisEffectPassiv)),
                new SkillEffectData(typeof(SymbiosisEffectPassiv), ClassNames.Druid)
            },
            {
                GetNameFromType(typeof(OvergrowthEffect)),
                new SkillEffectData(typeof(OvergrowthEffect), ClassNames.Druid)
            },
            {
                GetNameFromType(typeof(LifeLeechEffect)),
                new SkillEffectData(typeof(LifeLeechEffect), ClassNames.Druid)
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

            #region Warden
            {
                GetNameFromType(typeof(InterventionEffect)),
                new SkillEffectData(typeof(InterventionEffect), ClassNames.Warden)
            },
            {
                GetNameFromType(typeof(ArmorOverloadEffect)),
                new SkillEffectData(typeof(ArmorOverloadEffect), ClassNames.Warden)
            }
            #endregion /* Warden */
        };

        public static string GetNameFromType(Type type) { return type.ToString().Replace("ArcaneRecursion.", ""); }

        public static List<SkillData> GenerateInnateSkills(List<string> skills)
        {
            List<SkillData> result = new List<SkillData>();

            foreach (string skillName in skills)
                result.Add(InnateSkills[skillName]);

            return result;
        }
    }

    [Serializable]
    public class SkillData
    {
        public string Name { get; }
        public Type Skill;
        public SkillDefinition SkillDefinition;

        public SkillData(Type skill)
        {
            Name = SkillLibrary.GetNameFromType(skill);
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
        public SkillDefinition EffectDefinition;

        public SkillEffectData(Type skill, ClassNames className)
        {
            Name = SkillLibrary.GetNameFromType(skill);
            Class = className;
            Skill = skill;
            EffectDefinition = null;
        }
    }
}
