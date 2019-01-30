using System;

namespace JWA_Randomizer.Enums
{
    [Flags]
    public enum DinosaurType
    {
        Invalid = 0,

        Chomper = 1, // Heavy-hitting, moderate health, usually anti-armor and/or anti-shield, but slow
        Tank = 2, // High health, armor, and/or shielding abilities
        Raptor = 4, // Fast, deadly, but fragile.  Glass cannon
        Dotter = 8, // Uses damage over time abilities
        WildCard = 16, // Hard to classify.  Niche uses.  Typically a swap-in-strike ability
        AntiRaptor = 32, // Moderate health, armor, and speed.  Has methods to slow raptors down
        CounterAttack = 64, // Has the counter-attack passive
        Stunner = 128 // Has abilities that have a chance to stun
    }
}