/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    public enum SceneName
    {
        Initialize,
        Menu,
        InGame,
        ZTEST,
        LoadingScene,
        TownScene,
        DungeonScene,
    }

    public enum PoolObject
    {
        Spider,
        Zombie,
        Skeleton,
        BossOrc,
        EnemyHitEffect,
        PlayerHitEffect,
        HpParticle,
        DangerZone,
    }

    public enum PlayerState
    {
        Idle,
        Move,
        Attack,
        Evade,
        Stun,
        Death,
    }

    public enum PlayerWeaponState
    {
        None,
        Equip,
        Unequip,
    }

    public enum InputKey
    {
        none = 0,
        s = 1 << 0,
        f = 1 << 1,
        e = 1 << 2,
        q = 1 << 3,
        leftshift = 1 << 4,
        mouse0 = 1 << 5,
        mouse1 = 1 << 6,
    }

    public enum SkillName
    {
        None = 0,
        PistolBase = 1000,
        PistolTripleShot,
        PistolChargingShot,
        PistolCombo,
        SniperRifleChargingShot,
        SniperRifleCombo,
        BossWind = 2000,
        BossShootEnergy,
    }
    public enum PlayerAnimationParam
    {
        State,
        VelocityZ,
        VelocityX,
        Equip,
        Unequip,
    }

    public enum EnemyState
    {
        Idle,
        Follow,
        Return,
        Attack,
        GetDamage,
        Death,
    }
    public enum EnemyAnimationParam
    {
        State,
        Attack,
        GetDamage,
        Death,
        UseSkill,
    }
    public enum QuestState
    {
        NotAcceptable,
        Acceptable,
        During,
        Clear,
    }
    public enum SoundClipName
    {
        PlayerShoot,
        PlayerMelee,
        FootStepGrass,
        FootStepWood,
        FootStepTile,
        PlayerEvade,
        EnemyAttack,
        EnemyHit,
        ClearQuest,
        LevelUp,
    }
    public enum QuestImage
    {
        None,
        Acceptable,
        Clear,
    }
}
