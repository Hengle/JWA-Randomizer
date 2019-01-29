using System;

namespace JWA_Randomizer.Enums
{
    [Flags] public enum DinosaurType
    {
        Invalid = 0,
        Chomper = 1, // Hard-hitting, slow, usually anti-tank
        Tank = 2, // High health, armor, or shielding
        Raptor = 4, // Quick, deadly, but fragile
        Dotter = 8, // Deals damage over time
        WildCard = 16, // Surprise abilities, like swap-in-strike
        AntiRaptor = 32, // Moderate health with slowing and pinning abilities
        CounterAttack = 64, // Performs a counter-attack
        Stunner = 128 // Possibility to stun
    }
}
