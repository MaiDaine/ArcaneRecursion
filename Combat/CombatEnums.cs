using System;

namespace ArcaneRecursion
{
    #region Grid
    public enum BasicOrientation { Front, FrontSide, BackSide, Back }
    public enum HexDirection { NE, E, SE, SW, W, NW }
    public enum TileState { None, Invalid, Empty, Occupied };
    public enum TileTmpState { None, Invalid, Select, Hover, Path, SkillRange };
    public enum TileMaterial { Default, Select, Hover, Path, SkillTarget, Invalid }
    #endregion /* Grid */

    public enum SkillType { Passiv, Instant, Channel, Delayed }
    public enum TargetRequireType { Any, Valid, Empty, Unit, Allied, Enemy }
    public enum SkillMovementType { None, Directional, Projectile }
    public enum DamageTypes { None, Physical, Wind, Earth, Water, Fire, Mind, Arcane }
    public enum UnitAnimationType { Idle, Run }
}