using System;

namespace ArcaneRecursion
{
    #region Grid
    public enum BasicOrientation { Front, FrontSide, BackSide, Back }
    public enum HexDirection { NE, E, SE, SW, W, NW }
    public enum TileMaterial { Default, Select, Hover, Path, SkillTarget, Invalid }
    public enum TileState { None, Invalid, Empty, Occupied };
    public enum TileTmpState { None, Invalid, Select, Hover, Path, SkillRange };
    #endregion /* Grid */

    #region  Skill
    public enum DamageTypes { None, Physical, Wind, Earth, Water, Fire, Mind, Arcane }
    public enum SkillCursorType { None, Directional, Projectile, Radial }
    public enum SkillType { Passiv, Instant, Channel, Delayed }
    public enum SkillTag { AOE, Buff, Control, Damage, Debuff, Def, Heal, Move }
    public enum TargetRequireType { Any, Valid, Empty, Unit, Allied, Enemy }
    #endregion /* Skill */

    #region IA
    public enum TeamGoal { Poke, Burst, Defend }
    public enum FormationPosition { Front, MidFront, MidBack, Back }
    #endregion /* IA */

    public enum UnitAnimationType { Idle, Run }
}