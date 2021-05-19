using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace ArcaneRecursion
{
    public class CombatLoader : MonoBehaviour //TODO {Encounters}
    {
        public CombatLoaderData Data;
        public NodeDefinition NotImplemented;
        public List<ICombatTurnEntity> PlayerUnits;
        public List<ICombatTurnEntity> EnemyUnits;
        public List<ICombatTurnEntity> MapEntities;

        private List<int> POS = new List<int>() { 36,/* 24, 48, */47/*, 35, 59*/ };

        public void Init()
        {
            PlayerUnits = new List<ICombatTurnEntity>(Data.PlayerUnits.Count);
            int tmp = 0;
            foreach (CombatUnit e in Data.PlayerUnits)
            {
                GameObject unit = Instantiate(e.Prefab);
                UnitController controller = unit.GetComponent<UnitController>();
                CombatEntity combatEntity = new CombatEntity(unit, e, new CombatTurnEntityStore(1, tmp, e.Icone));
                //CombatEntity combatEntity = new CombatEntity(unit, e, 1, tmp);
                controller.Init(e, combatEntity, CombatGrid.Instance.Tiles[POS[tmp]]);
                controller.Movement.SetOrientation(controller.CurrentTile.SearchData.GetNeighbor(HexDirection.E));
                PlayerUnits.Add(combatEntity);
                tmp++;
            }

            EnemyUnits = new List<ICombatTurnEntity>(Data.EnemyUnits.Count);
            foreach (AICombatUnit e in Data.EnemyUnits)
            {
                GameObject unit = Instantiate(e.Prefab);
                unit.AddComponent<UnitBrain>();
                UnitController controller = unit.GetComponent<UnitController>();
                CombatEntity combatEntity = new AICombatEntity(unit, e, new CombatTurnEntityStore(2, tmp, e.Icone, e.FormationPosition));
                //CombatEntity combatEntity = new CombatEntity(unit, e, 2, tmp);
                controller.Init(e, combatEntity, CombatGrid.Instance.Tiles[POS[tmp]]);
                controller.Movement.SetOrientation(controller.CurrentTile.SearchData.GetNeighbor(HexDirection.W));
                EnemyUnits.Add(combatEntity);
                tmp++;
            }
            MapEntities = new List<ICombatTurnEntity>();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()//TODO UNIT TEST
        {
            SkillDefinition notImplemented = Resources.Load("NotImplemented") as SkillDefinition;
            ClassNames[] classNames = (ClassNames[])ClassNames.GetValues(typeof(ClassNames));
            string skillName;

            for (int classIndex = 1; classIndex < classNames.Length; classIndex++)
                for (int skillIndex = 0; skillIndex < 6; skillIndex++)
                {
                    skillName = ClassSkillLibrary.ClassSkillsDatas[classNames[classIndex]][skillIndex].Name;
                    SkillDefinition skillDefinition = Resources.Load<SkillDefinition>(string.Format(skillIndex == 0 ? "{0}/{1}/{1}Effect" : "{0}/{1}/{1}", classNames[classIndex].ToString(), skillName));
                    if (skillDefinition == null && skillName != "NotImplementedCombatSkill")
                    {
                        Debug.Log(string.Format("{0} => {1}", classNames[classIndex].ToString(), skillName));
                        Debug.Log(string.Format("{0}/{1}/{1}", classNames[classIndex].ToString(), skillName));
                    }
                    ClassSkillLibrary.ClassSkillsDatas[classNames[classIndex]][skillIndex].SkillDefinition = skillDefinition ?? notImplemented;
                }
            string[] stringSeparators = new string[1] { "Effect" };
            foreach (KeyValuePair<string, SkillEffectData> e in ClassSkillLibrary.ClassEffectsDatas)
            {
                string folderName = e.Value.Name.Split(stringSeparators, System.StringSplitOptions.RemoveEmptyEntries)[0];
                e.Value.EffectDefinition = Resources.Load(string.Format("{0}/{1}/{2}", classNames[(int)e.Value.Class].ToString(), folderName, e.Value.Name)) as SkillDefinition;
            }
        }
    }
}