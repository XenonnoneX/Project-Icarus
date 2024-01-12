public interface Ability
{
    delegate void OnAbilityUsed();
    event OnAbilityUsed onAbilityUsed;
}