using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerHealthSystem
{
    private PlayerController playerController;
    public static int BaseMaxDamageThreshold = 100;
    public int MaxDamageThreshold;
    public int CurrentDamage;
    public Dictionary<PlayerStatType, float> CalculatedStats { get; private set; } = new Dictionary<PlayerStatType, float>();

    public List<PlayerLimb> PlayerLimbs { get; set; } = new List<PlayerLimb>();

    public PlayerHealthSystem(PlayerController controller)
    {
        playerController = controller;
        InitialiseLimbs();
        InitialiseModules();
        InitialiseStats();
        CalculateStats();
    }

    private void InitialiseLimbs()
    {
        // These are some quick and dirty hardcoded things that should be replaced with dynamic changes from player action in the game/ui
        PlayerLimbs = new List<PlayerLimb>
        {
            new PlayerLimb { 
                limbType = LimbType.Head, 
                ModifiedStat = PlayerStatType.VisionRange, 
                Modifier = 0.5f, ModifierWhenDestroyed = -0.1f 
            },
            new PlayerLimb { 
                limbType = LimbType.Core, 
                ModifiedStat = PlayerStatType.DamageMitigation, 
                Modifier = 0.2f, 
                ModifierWhenDestroyed = -0.1f 
            },
            new PlayerLimb { 
                limbType = LimbType.Arm, 
                ModifiedStat = PlayerStatType.OutgoingDamage, 
                Modifier = 0.25f, 
                ModifierWhenDestroyed = -0.15f 
            },
            new PlayerLimb { 
                limbType = LimbType.Arm, 
                ModifiedStat = PlayerStatType.OutgoingDamage, 
                Modifier = 0.25f, 
                ModifierWhenDestroyed = -0.15f 
            },
            new PlayerLimb { 
                limbType = LimbType.Leg, 
                ModifiedStat = PlayerStatType.MoveSpeed, 
                Modifier = 0.15f, 
                ModifierWhenDestroyed = -0.1f 
            },
            new PlayerLimb { 
                limbType = LimbType.Leg, 
                ModifiedStat = PlayerStatType.MoveSpeed, 
                Modifier = 0.15f, 
                ModifierWhenDestroyed = -0.1f 
            }
        };

    }
    private void InitialiseModules() 
    { 
        // Attach modules to all installed limbs based on type. This is dumb and just for testing.
        PlayerLimbs.Find(l => l.limbType == LimbType.Leg)?
            .InstalledModules.Add(new LimbModule { 
                Name = "Speed Boost", 
                ModifiedStat = PlayerStatType.MoveSpeed, 
                Modifier = 0.1f });
        PlayerLimbs.Find(l => l.limbType == LimbType.Arm)?
            .InstalledModules.Add(new LimbModule { 
                Name = "Power Grip", 
                ModifiedStat = PlayerStatType.OutgoingDamage, 
                Modifier = 0.2f });
        PlayerLimbs.Find(l => l.limbType == LimbType.Core)?
            .InstalledModules.Add(new LimbModule { 
                Name = "Reinforced Plating", 
                ModifiedStat = PlayerStatType.DamageMitigation, 
                Modifier = 0.3f });
        PlayerLimbs.Find(l => l.limbType == LimbType.Head)?
            .InstalledModules.Add(new LimbModule { 
                Name = "Enhanced Optics", 
                ModifiedStat = PlayerStatType.VisionRange, 
                Modifier = 2.0f });
    }

    void InitialiseStats()
    {
        MaxDamageThreshold = BaseMaxDamageThreshold;
        CurrentDamage = 0;
        CalculateStats();
    }

    public void CalculateStats()
    {
        // Todo: Anything from our unlocked upgrades that modifies stats should
        // calculate here too, as well as base/limb/module etc

        CalculatedStats.Clear();

        foreach (PlayerStatType statType in Enum.GetValues(typeof(PlayerStatType)))
        {
            float totalModifier = 0f;

            foreach (var limb in PlayerLimbs)
            {
                if (limb.ModifiedStat == statType)
                {
                    totalModifier += (limb.ActiveState == LimbState.Alive) ? limb.Modifier : limb.ModifierWhenDestroyed;
                }

                foreach (var module in limb.InstalledModules)
                {
                    if (module.ModifiedStat == statType)
                        totalModifier += module.Modifier;
                }
            }

            CalculatedStats[statType] = totalModifier;
        }
    }

    public void TakeDamage(int RawIncoming)
    {
        // Current implementation assumes that all pre-mitigation damage calcs
        // are done BEFORE passing that value to this method.
        // i.e. we don't pass it the bullet/projectile/whatever prefab, we figure out how much damage
        // is thrown and then throw that value here.
        float damageMitigation = CalculatedStats.ContainsKey(PlayerStatType.DamageMitigation) 
                                 ? CalculatedStats[PlayerStatType.DamageMitigation] 
                                 : 0f;
        int MitigatedDamage = Mathf.RoundToInt(RawIncoming * (1 - damageMitigation));
        CurrentDamage += MitigatedDamage;

        if (CurrentDamage > MaxDamageThreshold)
        {
            // Account for edge case where we take MASSIVE damage, multiples of the max threshold
            while (CurrentDamage >= MaxDamageThreshold)
            {
                DestroyLimb();
                CurrentDamage -= MaxDamageThreshold;
            }
            // UpdateTheUiToShowNewDamageAmount();
        }
        Debug.Log($"Incoming Damage: {RawIncoming}, Mitigated Damage: {MitigatedDamage}, Current Damage: {CurrentDamage}");

    }

    public void DestroyLimb()
    {
        // TriggerSomeAnimationOfLimbGoingByeBye();
        // UpdateTheUiToShowDeadLimbOnHUD();
        var aliveLimbs = PlayerLimbs.Where(l => l.ActiveState == LimbState.Alive).ToList();
        if (aliveLimbs.Count <= 1) // If we're about to destroy a limb and there's only one left, GG
        {
            playerController.OnPlayerDeath(); // This onplayerdeath method should probably live in a higher level GameManager than the player controller, idk
        }

        var limbToDestroy = aliveLimbs[UnityEngine.Random.Range(0, aliveLimbs.Count)]; // I don't remember if we need to do 0 to count-1
        limbToDestroy.ActiveState = LimbState.Destroyed;
        limbToDestroy.InstalledModules.Clear();

        Debug.Log($"Limb {limbToDestroy.limbType} has been destroyed and its modules removed.");

        CalculateStats();
    }

}

// Replace this when we add a more robust modifier system
public enum PlayerStatType
{
    MaxHealth,
    MoveSpeed,
    DamageMitigation,
    VisionRange,
    OutgoingDamage,
    RateOfFire
}
public enum LimbState
{
    Alive,
    Destroyed
}
public enum LimbType
{
    None,
    Any,
    Arm,
    Leg,
    Core,
    Head
}

public class PlayerLimb
{
    // Todo: Upgrades (e.g. researched perma bonuses to stats that persist through destruction.)

    public Sprite CharacterSprite;
    public Vector2 CharacterSpritePos = new Vector2(0, 0); // Probably implement this as relative to character center and nudge x/y as appropriate
    public Sprite UiSprite;
    public Vector2 UiSpritePos = new Vector2(0, 0);

    public LimbType limbType = LimbType.Leg; // Was tempted to name this IsA. ThisLimb.IsA Arm. I am dumb.
    public LimbState ActiveState = LimbState.Alive;

    // Replace this when we add a more robust modifier system
    public PlayerStatType ModifiedStat = PlayerStatType.MoveSpeed;    
    public float Modifier = 0.15f;
    public PlayerStatType ModifiedWhenDestroyed = PlayerStatType.MoveSpeed;
    public float ModifierWhenDestroyed = -0.05f;

    public List<LimbModule> InstalledModules = new List<LimbModule>();

}

public class LimbModule
{
    // Todo: Upgrades (e.g. researched perma bonuses to stats that persist through destruction.)

    public Sprite UiSprite; // in my head this is like a spell/item icon in wow, only seen in ui
    public string Name = "Default Module";
    public string Description = "This is a default module instance. Somebody forgot to update it!";

    public LimbType FitsIn = LimbType.Any;

    // Replace this when we add a more robust modifier system
    public PlayerStatType ModifiedStat = PlayerStatType.MaxHealth;
    public float Modifier = 0.5f;
}