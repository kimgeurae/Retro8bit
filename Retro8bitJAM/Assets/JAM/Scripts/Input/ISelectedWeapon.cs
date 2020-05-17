using JAM.Scripts.Weapons;

namespace JAM.Scripts.Input
{
    public interface ISelectedWeapon
    {
        BaseWeapon CurrentWeapon { get; }
    }
}